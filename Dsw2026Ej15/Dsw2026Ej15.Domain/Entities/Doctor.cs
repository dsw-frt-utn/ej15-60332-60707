using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string _name { get; set; } = "";
        public string _licenseNumber { get; set; } = "";
        public bool _isActive { get; set; }
        public Speciality _speciality { get; set; }

        public Doctor() { }
        public Doctor(string name, string licenseNumber, bool isActive, Speciality speciality)
        {
            _name = name;
            _licenseNumber = licenseNumber;
            _isActive = isActive;
            _speciality = speciality;
        }

    }
}
