using Dsw2026Ej15.Api.Dtos;
using Dsw2026Ej15.Domain.Entities;
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

    public IActionResult AddDoctor(AddDoctorDTO values)
    {
        var especialidad = Persistence.GetEspecialidad(values.specialityId);

        if (especialidad is null)
        {
            return BadRequest(new { message = "El ID de especialidad es incorrecto." });
        }

        var resultado = Persistence.AgregarDoctor(new Doctor(Guid.NewGuid(), values.name, values.licenseNumber, true, especialidad));

        if (!resultado)
        {
            return BadRequest(new { message = "No se pudo agregar al doctor." });
        }

        return Created();
    }

    [HttpGet]
    public IActionResult GetDoctors()
    {
        var doctors = Persistence.GetDoctores();
        var doctoresActivos = doctors.Where(doctor => doctor.IsActive);

        return Ok(doctoresActivos);
    }

    [HttpGet("{id}")]
    public IActionResult GetDoctor(Guid id)
    {
        var doctor = Persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        return Ok(new DoctorResponseDTO(doctor.Name, doctor.LicenseNumber, doctor.Speciality.Name));
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = Persistence.GetDoctor(id);

        if (doctor == null || !doctor.IsActive)
        {
            return NotFound();
        }

        doctor.ActualizarParcial(isActive: false);

        Persistence.ActualizarDoctor(doctor);

        return NoContent();
    }
}
