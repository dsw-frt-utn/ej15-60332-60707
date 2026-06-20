using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistencia;
    public DoctorsController(IPersistence persistencia)
    {
        _persistencia = persistencia;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDoctor([FromBody] DoctorModel.Request request)
    {
      
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                throw new ValidationException("Nombre y Matrícula requeridos");
            }

            var speciality = await _persistencia.GetSpecialityByIdAsync(request.SpecialityId);
            if (speciality == null)
            {
                throw new ValidationException("Especialidad no existe");
            }

            var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
            await _persistencia.AddDoctorAsync(doctor);

            return Created();


    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {

            var doctor = await _persistencia.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
            {
                throw new NotFoundException("El médico no existe o no está activo.");

            }        

            return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name ?? ""));


    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorModel.Response>>> GetDoctor()
    {
        
            List<DoctorModel.Response> doctors = new List<DoctorModel.Response>();

            foreach (var d in await _persistencia.GetDoctorsAsync())
            {
                doctors.Add(
                    new DoctorModel.Response(d.Name, d.LicenseNumber, d.Speciality?.Name ?? ""));
            }
            return Ok(doctors);
        
        
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
     
            var doctor = await _persistencia.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
            {
                throw new NotFoundException("El médico no existe o no está activo.");
            }

            await _persistencia.DeleteDoctorAsync(id);
            return NoContent();
        
    }



}
