namespace Dsw2026Ej15.Domain.Entities;

public class Speciality : BaseEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Speciality(Guid id, string name, string description)
    {
        Name = name;
        Description = description;
        Id = id;
    }
}
