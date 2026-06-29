namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; private set; }
    public string LicenseNumber { get; private set; }
    public bool IsActive { get; private set; }
    public Guid SpecialityId { get; set; }
    public Speciality Speciality { get; private set; }

    private Doctor() { }

    public Doctor(Guid id, string name, string licenseNumber, bool isActive, Speciality speciality)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        IsActive = isActive;
        Speciality = speciality;
        Id = id;
    }

    public void Actualizar(Doctor doctor)
    {
        Name = doctor.Name;
        LicenseNumber = doctor.LicenseNumber;
        IsActive = doctor.IsActive;
        Speciality = doctor.Speciality;
        Id = doctor.Id;
    }

    public void ActualizarParcial(string? name = null, string? licenseNumber = null, bool? isActive = null)
    {
        if (isActive is not null)
        {
            IsActive = (bool)isActive;
        }

        if (licenseNumber is not null)
        {
            LicenseNumber = licenseNumber;
        }

        if (name is not null)
        {
            Name = name;
        }
    }
}
