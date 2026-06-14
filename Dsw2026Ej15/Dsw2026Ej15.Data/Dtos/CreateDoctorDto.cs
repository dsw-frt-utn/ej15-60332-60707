using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos
{
    public class CreateDoctorDto
    {
        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "The license number is required.")]
        public string LicenseNumber { get; set; } = "";

        public Guid SpecialityId { get; set; }
    }
}
