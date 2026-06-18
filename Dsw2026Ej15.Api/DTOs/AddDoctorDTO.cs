namespace Dsw2026Ej15.Api.Dtos;

public record class AddDoctorDTO(string name, string licenseNumber, Guid specialityId);