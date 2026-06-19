using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        public List<Doctor> GetDoctors();
        public Doctor? GetDoctor(Guid id);
        public bool AgregarDoctor(Doctor doc);
        public bool ActualizarDoctor(Doctor doc);
        public Speciality? GetSpeciality(Guid id);
        public Doctor? GetDoctorByLicenseNumber(string licenseNumber);
    }
}
