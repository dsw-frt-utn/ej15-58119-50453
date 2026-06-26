using Dsw2026Ej15.Api.DTOs;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private IPersistence Persistence { get; set; }

    public DoctorsController(IPersistence persistence)
    {
        Persistence = persistence;
    }

    [HttpPost]

    public async Task<IActionResult> AddDoctor(DoctorDTO.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.LicenseNumber) || string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ValidationException("El nombre y/o la matricula estan vacios.");
        }

        if (Persistence.GetDoctorByLicenseNumber(request.LicenseNumber) is not null)
        {
            throw new ValidationException("La matricula pertenece a otro doctor.");
        }

        var especialidad = await Persistence.GetSpeciality(request.SpecialityId) ?? throw new ValidationException("El ID de especialidad es incorrecto.");

        await Persistence.AgregarDoctor(new Doctor(Guid.NewGuid(), request.Name, request.LicenseNumber, true, especialidad));

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetDoctors()
    {
        var doctors = await Persistence.GetDoctors();
        return Ok(doctors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctor(Guid id)
    {
        var doctor = await Persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        return Ok(new DoctorDTO.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality.Name));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var doctor = await Persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        doctor.ActualizarParcial(isActive: false);

        await Persistence.ActualizarDoctor(doctor);

        return NoContent();
    }
}
