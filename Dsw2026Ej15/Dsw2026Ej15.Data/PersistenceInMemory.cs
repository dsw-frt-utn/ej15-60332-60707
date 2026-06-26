using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;


namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Speciality> _specialities = [];
        private List<Doctor> _doctors = [];

        public PersistenceInMemory() 
        {
             LoadSpecialities();
        }

        private void LoadSpecialities()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "specialities.json");
                var json = File.ReadAllText(jsonPath);
                var especialidadesData = JsonSerializer.Deserialize<List<SpecialityDto>>(json, 
                    new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? [];

                _specialities = [.. especialidadesData.Select(s => new Speciality(s.Name, s.Description, s.Id))];

            }
            catch (Exception)
            {

            }
        }

                
        public async Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return await Task.FromResult(_specialities.SingleOrDefault(s => s.Id == id));
        }


        public async Task AddDoctorAsync(Doctor doctor)
        {
            await Task.Run(() => _doctors.Add(doctor));
        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
            return await Task.FromResult(_doctors.FirstOrDefault(d => d.Id == id));
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync()
        {
            return await Task.FromResult(_doctors);

        }

        public async Task DeleteDoctorAsync(Guid id)
        {
            foreach(Doctor d in _doctors)
            {
                if (d.Id == id)
                {
                    d.Deactivate();
                    break;
                }
            }
        }
    }
}
