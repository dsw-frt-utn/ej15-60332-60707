using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEF : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;
        public PersistenceEF(Dsw2026Ej15DbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync() 
        {
            return _context.Doctors
                .Include(nameof(Doctor.Speciality))
                .Where(d => d.IsActive);

        }

        public async Task<Doctor?> GetDoctorByIdAsync(Guid id)
        {
           return await _context.Doctors
                .Include(d => d.Speciality)
                .SingleOrDefaultAsync(d => d.Id == id && d.IsActive);
        }

        public Task<Speciality?> GetSpecialityByIdAsync(Guid id)
        {
            return _context.Specialities.SingleOrDefaultAsync(s => s.Id == id);
        }

        public async Task SaveDoctorAsync(Doctor doctor)
        {
            _context.Add(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _context.SaveChangesAsync();
        }
    }
}
