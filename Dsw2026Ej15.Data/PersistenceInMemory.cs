using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> Doctores = [];
        private readonly List<Speciality> Especialidades = [];

        public PersistenceInMemory()
        {
            LoadData();
        }

        public void LoadData()
        {
            LoadDoctors();
            LoadSpecialities();
        }

        private void LoadDoctors()
        {
            var doctoresDatos = CargarDatosDeArchivos<DoctorDtos>("doctors");
            if(doctoresDatos != null)
            {
                foreach(var dato in doctoresDatos)
                {
                    var speciality = Especialidades.Find(s => s.Id == dato.SpecialityId);
                    if(speciality != null)
                    {
                        Doctor doc = new Doctor(dato.Id, dato.Name, dato.LicenseNumber, dato.IsActive, speciality);
                        Doctores.Add(doc);
                    }
                }
            }
        }

        private void LoadSpecialities()
        {
            var especDatos = CargarDatosDeArchivos<SpecialityDtos>("specialities");
            if (especDatos != null)
            {
                foreach(var dato in especDatos)
                {
                    Speciality speciality = new Speciality(dato.Id, dato.Name, dato.Description);
                    Especialidades.Add(speciality);
                }
            }
        }
        private List<T>? CargarDatosDeArchivos<T>(string file)
        {
            string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
            string jsoncontent = File.ReadAllText(jsonpath);
            return JsonSerializer.Deserialize<List<T>>(jsoncontent);
        }

        public List<Doctor> GetDoctores()
        {
            return Doctores;
        }

        public Doctor? GetDoctor(Guid id)
        {
            return Doctores.Find(d => (d.Id == id && d.IsActive));
        }
        public bool AgregarDoctor(Doctor doc)
        {
            try
            {
                Doctores.Add(doc);
                return true;
            }
            catch
            {
                return false;
            }
        }
        
    }
}
