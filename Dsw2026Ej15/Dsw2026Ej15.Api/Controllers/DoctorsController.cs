using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Data.Dtos;
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
        try
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

            return Created("", new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, speciality.Name));
        }
        catch (ValidationException ex) 
        {
            return BadRequest(ex.Message);
        }

    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        try
        {
            var doctor = await _persistencia.GetDoctorByIdAsync(id);

            if (doctor == null || !doctor.IsActive)
            {
                throw new ValidationException("El médico no existe o no está activo.");
            }

            return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name ?? ""));

        }
        catch (ValidationException ex)
        {
            return BadRequest(ex.Message);
        }

    }


    //[HttpPost]
    //public IActionResult AddDoctor([FromBody] CreateDoctorDto doctorDto)
    //{
    //    try
    //    {

    //        bool isSpecialityValid = _persistence.SpecialityExists(doctorDto.SpecialityId);
    //        if (isSpecialityValid == false)
    //        {
    //            return BadRequest("Invalid speciality.");
    //        }
    //        else
    //        {
    //            Doctor doctor = new Doctor(doctorDto.Name, doctorDto.LicenseNumber, true, _persistence.GetSpecialityById(doctorDto.SpecialityId));
    //            _persistence.AddDoctor(doctor);
    //            return StatusCode(201, doctor);
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpGet]
    //public ActionResult<IEnumerable<GetDoctorDto>> GetDoctor()
    //{
    //    try
    //    {
    //        List<GetDoctorDto> doctors = new List<GetDoctorDto>();

    //        foreach (var d in _persistence.GetDoctors())
    //        {
    //            doctors.Add(
    //                new GetDoctorDto(d.Id, d.Name, d.LicenseNumber, d.Speciality?.Name ?? ""));
    //        }
    //        return Ok(doctors);
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpGet("{id}")]
    //public IActionResult GetDoctorById(Guid id)
    //{
    //    try
    //    {
    //        var doctor = _persistence.GetDoctorById(id);

    //        if (doctor == null || !doctor.IsActive)
    //        {
    //            return NotFound("El médico no existe o no está activo.");
    //        }

    //        var dto = new GetDoctorDto(
    //            doctor.Id, doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name ?? "");
    //        return Ok(dto);
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}

    //[HttpDelete("{id}")]
    //public IActionResult DeleteDoctor(Guid id)
    //{
    //    try
    //    {
    //        var doctor = _persistence.GetDoctorById(id);

    //        if (doctor == null || !doctor.IsActive)
    //        {
    //            return NotFound("El médico no existe o no está activo.");
    //        }

    //        _persistence.DeleteDoctor(id);
    //        return NoContent();
    //    }
    //    catch (Exception e)
    //    {
    //        return BadRequest(e.Message);
    //    }
    //}



}
