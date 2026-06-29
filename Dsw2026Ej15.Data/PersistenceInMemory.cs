using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
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

        public async Task LoadData()
        {
            LoadSpecialities();
            await LoadDoctors();
        }

        private async Task LoadDoctors()
        {
            try
            {
                var doctoresDatos = CargarDatosDeArchivos<DoctorDtos>("doctors");
                if (doctoresDatos != null)
                {
                    foreach (var dato in doctoresDatos)
                    {
                        var speciality = await GetSpeciality(dato.SpecialityId);
                        if (speciality != null)
                        {
                            Doctor doc = new Doctor(dato.Id, dato.Name, dato.LicenseNumber, dato.IsActive, speciality);
                            _doctors.Add(doc);
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Ocurrio un error al cargar los medicos.");
            }

        }

        private void LoadSpecialities()
        {
            try
            {
                var especDatos = CargarDatosDeArchivos<SpecialityDtos>("specialities");
                _specialities = [.. especDatos?.Select(speciality => new Speciality(speciality.Id, speciality.Name, speciality.Description)) ?? []];
            }
            catch
            {
                throw new Exception("Ocurrio un error al cargar las especialidades.");
            }

        }
        private List<T>? CargarDatosDeArchivos<T>(string file)
        {
            string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
            string jsoncontent = File.ReadAllText(jsonpath);
            return JsonSerializer.Deserialize<List<T>>(jsoncontent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) ?? [];
        }

        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return _doctors;
        }

        public async Task<Doctor?> GetDoctor(Guid id)
        {
            return _doctors.Find(d => d.Id == id);
        }

        public async Task<bool> AgregarDoctor(Doctor doc)
        {
            try
            {
                _doctors.Add(doc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> ActualizarDoctor(Doctor doctor)
        {
            var doctorGuardado = await GetDoctor(doctor.Id);

            if (doctorGuardado is null)
            {
                return false;
            }

            doctorGuardado.Actualizar(doctor);

            return true;
        }

        public async Task<Speciality?> GetSpeciality(Guid id)
        {
            return _specialities.FirstOrDefault(d => d.Id == id);
        }

        public async Task<Doctor?> GetDoctorByLicenseNumber(string licenseNumber)
        {
            return _doctors.Find(doctor => doctor.LicenseNumber == licenseNumber);
        }
    }
}
