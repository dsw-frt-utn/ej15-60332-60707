using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(Guid id);
        Task<Speciality?> GetSpecialityByIdAsync(Guid id);
        Task SaveDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
    }
}
