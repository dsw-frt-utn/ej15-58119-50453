using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory
    {
        private List<Speciality> _specialities = [];

        public PersistenceInMemory()
        {
            LoadData();
        }

        public void LoadData()
        {
            LoadSpecialities();
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

        

        
    }
}
