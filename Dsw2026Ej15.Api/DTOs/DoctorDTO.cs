namespace Dsw2026Ej15.Api.DTOs;

public record DoctorDTO
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);
    public record Response(string name, string licenseNumber, string specialityName);
}
