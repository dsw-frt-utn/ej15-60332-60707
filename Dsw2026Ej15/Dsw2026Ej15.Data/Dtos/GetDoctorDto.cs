using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos
{
    public class GetDoctorDto
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string LicenseNumber { get; set; } = "";
        public string SpecialityName { get; set; } = "";

        public GetDoctorDto(Guid id, string name, string licenseNumber, string specialityName)
        {
            Id = id;
            Name = name;
            LicenseNumber = licenseNumber;
            SpecialityName = specialityName;
        }

    }
}
