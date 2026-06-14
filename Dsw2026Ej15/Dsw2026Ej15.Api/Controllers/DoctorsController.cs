using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence _persistence;

        public DoctorsController(IPersistence persistence) {
            _persistence = persistence;
        }

        [HttpPost]
        public IActionResult AddDoctor([FromBody] CreateDoctorDto doctorDto)
        {
            try {

                bool isSpecialityValid = _persistence.SpecialityExists(doctorDto.SpecialityId);
                if (isSpecialityValid == false)
                {
                    return BadRequest("Invalid speciality.");
                }
                else
                {
                    Doctor doctor = new Doctor(doctorDto.Name, doctorDto.LicenseNumber,true, _persistence.GetSpecialityById(doctorDto.SpecialityId));
                    _persistence.AddDoctor(doctor);
                    return StatusCode(201, doctor);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<GetDoctorDto>> GetDoctor()
        {
            try
            {
                List<GetDoctorDto> doctors = new List<GetDoctorDto>();

                foreach (var d in _persistence.GetDoctors())
                {
                    if (d.IsActive == true) {
                        doctors.Add(new GetDoctorDto(d.Id, d.Name, d.LicenseNumber, d.Speciality?.Name ?? ""));
                    }
                }
                return Ok(doctors);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[HttpGet(Name = "PostDoctorsController")]
        //public IEnumerable<DoctorsController> POST()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new DoctorsController
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //.ToArray();
        //}

        //[HttpGet(Name = "GetDoctorsController")]
        //public IEnumerable<DoctorsController> GET()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new DoctorsController
        //    {
        //        Name = "",
        //        licenseNumber = "",
        //        specialityId = ""
        //    })
        //    .ToArray();
        //}

    }
}
