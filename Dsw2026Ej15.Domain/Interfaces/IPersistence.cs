using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor?> GetDoctor(Guid id);
        Task AgregarDoctor(Doctor doc);
        Task ActualizarDoctor(Doctor doc);
        Task<Speciality?> GetSpeciality(Guid id);
        Task<Doctor?> GetDoctorByLicenseNumber(string licenseNumber);
    }
}
