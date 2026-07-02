using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;

public class PersistenceEf : IPersistence
{
    private Dsw2026Ej15DbContext _context;

    public PersistenceEf(Dsw2026Ej15DbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<bool> ActualizarDoctor(Doctor doc)
    {
        var doctorGuardado = await GetDoctor(doc.Id);

        if (doctorGuardado is null)
        {
            return false;
        }

        _context.Doctors.Update(doc);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AgregarDoctor(Doctor doc)
    {
        try
        {
            var result = await _context.Doctors.AddAsync(doc);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<Doctor?> GetDoctor(Guid id)
    {
        return _context.Doctors.Include(doctor => doctor.Speciality).SingleOrDefault(doctor => doctor.Id == id && doctor.IsActive);
    }

    public async Task<Doctor?> GetDoctorByLicenseNumber(string licenseNumber)
    {
        return _context.Doctors.SingleOrDefault(doctor => doctor.LicenseNumber == licenseNumber && doctor.IsActive);
    }

    public async Task<IEnumerable<Doctor>> GetDoctors()
    {
        return _context.Doctors.Include(doctor => doctor.Speciality).Where(doctor => doctor.IsActive);
    }

    public async Task<Speciality?> GetSpeciality(Guid id)
    {
        return _context.Specialities.SingleOrDefault(speciality => speciality.Id == id);
    }
}