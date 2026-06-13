namespace Dsw2026Ej15.Domain.Entities;

public class Doctor : BaseEntity
{
    public string Name { get; private set; }
    public string LicenseNumber { get; private set; }
    public bool IsActive { get; private set; }
    public Speciality Speciality { get; private set; }

    public Doctor(Guid id, string name, string licenseNumber, bool isActive, Speciality speciality)
    {
        Name = name;
        LicenseNumber = licenseNumber;
        IsActive = isActive;
        Speciality = speciality;
        Id = id;
    }
}
