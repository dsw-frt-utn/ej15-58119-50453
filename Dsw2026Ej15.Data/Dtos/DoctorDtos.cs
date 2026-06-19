using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos
{
    internal record DoctorDtos(Guid Id, string Name, string LicenseNumber, bool IsActive, Guid SpecialityId);
    
}
