using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        public Task<IEnumerable<Doctor>> GetDoctors();
        public Task<Doctor?> GetDoctor(Guid id);
        public Task<bool> AgregarDoctor(Doctor doc);
        public Task<bool> ActualizarDoctor(Doctor doc);
        public Task<Speciality?> GetSpeciality(Guid id);
        public Task<Doctor?> GetDoctorByLicenseNumber(string licenseNumber);
    }
}
