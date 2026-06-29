using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data.Seed;

public static class DbSeeder
{
    public static async Task Seed(Dsw2026Ej15DbContext context)
    {
        await context.Database.MigrateAsync();

        await SeedSpecialities(context);
        await SeedDoctors(context);
    }

    private static async Task SeedSpecialities(Dsw2026Ej15DbContext context)
    {
        if (await context.Specialities.AnyAsync())
        {
            return;
        }

        var specialities = LoadJson<List<SpecialityDtos>>("specialities");

        if (specialities is null)
        {
            return;
        }

        foreach (var item in specialities)
        {
            var speciality = new Speciality(
                item.Id,
                item.Name,
                item.Description
            );

            context.Specialities.Add(speciality);
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedDoctors(Dsw2026Ej15DbContext context)
    {
        if (await context.Doctors.AnyAsync())
        {
            return;
        }

        var doctors = LoadJson<List<DoctorDtos>>("doctors");

        if (doctors is null)
        {
            return;
        }

        foreach (var item in doctors)
        {
            var speciality = await context.Specialities
                .SingleOrDefaultAsync(s => s.Id == item.SpecialityId);

            if (speciality is null)
            {
                continue;
            }

            var doctor = new Doctor(
                item.Id,
                item.Name,
                item.LicenseNumber,
                item.IsActive,
                speciality
            );

            context.Doctors.Add(doctor);
        }

        await context.SaveChangesAsync();
    }

    private static T? LoadJson<T>(string fileName)
    {
        var path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Sources",
            $"{fileName}.json"
        );

        if (!File.Exists(path))
        {
            return default;
        }

        var json = File.ReadAllText(path);

        return JsonSerializer.Deserialize<T>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        );
    }
}