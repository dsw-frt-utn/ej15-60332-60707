using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        List<Speciality> GetSpeciality();

        List<Doctor> GetDoctors();
        void AddDoctor(Doctor doctor);
        Doctor? GetDoctorById(Guid id);
        void DeleteDoctor(Guid id);

        bool SpecialityExists(Guid id);

        Speciality GetSpecialityById(Guid id);
    }
}
