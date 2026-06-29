using Dsw2026Ej15.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data;
public class Dsw2026Ej15DbContext : DbContext
{
    public DbSet<Speciality> Specialities { get; set; }

    public DbSet<Doctor> Doctors { get; set; }
    public string DbPath { get; }

    public Dsw2026Ej15DbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "dsw2026ej15.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}