using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data;

public class PersistenceEF : IPersistence
{
    private readonly Dsw2026Ej15DbContext _context;
    public PersistenceEF(Dsw2026Ej15DbContext context)
    {
        _context = context;
    }
    public async Task ActualizarDoctor(Doctor doc)
    {
        var doctor = await GetDoctor(doc.Id);
        if(doctor is null)
        {
            throw new Exception("No se encontro al doctor");

        }
        doctor.Actualizar(doc);

    }

    public async Task AgregarDoctor(Doctor doc)
    {
        await _context.Doctors.AddAsync(doc);
    }

    public async Task<Doctor?> GetDoctor(Guid id)
    {
        return await _context.Doctors.FindAsync(id);
    }

    public async Task<Doctor?> GetDoctorByLicenseNumber(string licenseNumber)
    {
        return await _context.Doctors.FindAsync(licenseNumber);
        
    }

    public async Task<IEnumerable<Doctor>> GetDoctors()
    {
        return _context.Doctors.Where(d => d.IsActive);
    }

    public async Task<Speciality?> GetSpeciality(Guid id)
    {
        return _context.Specialities.FirstOrDefault(d => d.Id == id);
    }
}
