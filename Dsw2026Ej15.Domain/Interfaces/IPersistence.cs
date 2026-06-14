using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        public List<Doctor> GetDoctores();
        public Doctor? GetDoctor(Guid id);
        public bool AgregarDoctor(Doctor doc);

    }
}
