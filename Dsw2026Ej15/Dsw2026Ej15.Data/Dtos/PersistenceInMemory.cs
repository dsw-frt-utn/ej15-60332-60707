using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace Dsw2026Ej15.Data.Dtos
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Speciality> _specialities = [];
        private readonly List<Doctor> _doctors = [];

        public PersistenceInMemory() 
        {
             LoadSpecialities();
        }
        private void LoadSpecialities()
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json"); 
            var json = File.ReadAllText(jsonPath);
            var especialidadesData = JsonSerializer.Deserialize<List<Speciality>>(json);
 
            if (especialidadesData != null)
            {
                foreach (var data in especialidadesData)
                {
                    Speciality r = new(data.Id, data.Name, data.Description);
                    _specialities.Add(r);
                }
            }
        }
        public List<Speciality> GetSpeciality()
        {
            return _specialities;
        }
        
        public List<Doctor> GetDoctors()
        {
            return _doctors.Where(d => d.IsActive).ToList();
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(d => d.Id == id); 
        }

        public void DeleteDoctor(Guid id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (doctor != null)
            {
                doctor.IsActive = false;
            }
          
        }

    }
}
