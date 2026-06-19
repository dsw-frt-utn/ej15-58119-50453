using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> _doctors = [];
        private List<Speciality> _specialities = [];

        public PersistenceInMemory()
        {
            LoadData();
        }

        public void LoadData()
        {
            LoadSpecialities();
            LoadDoctors();
        }

        private void LoadDoctors()
        {
            var doctoresDatos = CargarDatosDeArchivos<DoctorDtos>("doctors");
            if (doctoresDatos != null)
            {
                foreach (var dato in doctoresDatos)
                {
                    var speciality = GetSpeciality(dato.SpecialityId);
                    if (speciality != null)
                    {
                        Doctor doc = new Doctor(dato.Id, dato.Name, dato.LicenseNumber, dato.IsActive, speciality);
                        _doctors.Add(doc);
                    }
                }
            }
        }

        private void LoadSpecialities()
        {
            try
            {
                var especDatos = CargarDatosDeArchivos<SpecialityDtos>("specialities");
                _specialities = [.. especDatos.Select(s=>new Speciality(s.Id,s.Name,s.Description))];
            }
            catch(Exception)
            {

            }
            
        }
        private List<T>? CargarDatosDeArchivos<T>(string file)
        {
            string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
            string jsoncontent = File.ReadAllText(jsonpath);
            return JsonSerializer.Deserialize<List<T>>(jsoncontent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
        }

        public List<Doctor> GetDoctors()
        {
            return _doctors;
        }

        public Doctor? GetDoctor(Guid id)
        {
            return _doctors.Find(d => d.Id == id);
        }
        public bool AgregarDoctor(Doctor doc)
        {
            try
            {
                if(_doctors.Find(d => d.LicenseNumber == doc.LicenseNumber) is not null)
                {
                    throw new Exception();
                }
                _doctors.Add(doc);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool ActualizarDoctor(Doctor doctor)
        {
            var doctorGuardado = GetDoctor(doctor.Id);

            if (doctorGuardado is null)
            {
                return false;
            }

            doctorGuardado.Actualizar(doctor);

            return true;
        }

        public Speciality? GetSpeciality(Guid id)
        {
            return _specialities.FirstOrDefault(d => d.Id == id);
        }
    }
}
