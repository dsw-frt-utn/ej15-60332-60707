using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<Speciality?> GetSpecialityByIdAsync(Guid id);


        Task AddDoctorAsync(Doctor doctor);
        Task<Doctor?> GetDoctorByIdAsync(Guid id);

        
    }
}
