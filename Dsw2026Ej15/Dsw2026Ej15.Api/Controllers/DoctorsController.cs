using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Dsw2026Ej15.Api.Controllers;

[Route("doctors")]
public class DoctorsController : ApiController
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
            await _persistencia.SaveDoctorAsync(doctor);

            return Created();
    }

    [HttpGet]
    public async Task<ActionResult> GetAllDoctors()
    {

        var doctors = await _persistencia.GetAllDoctorsAsync();
        return Ok(doctors.Select(d => new DoctorModel.Response(d.Name, d.LicenseNumber, d.Speciality?.Name)));

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById([FromRoute]Guid id)
    {

        var doctor = (await GetDoctor(id));
        return Ok(new DoctorModel.Response(doctor.Name, doctor.LicenseNumber, doctor.Speciality?.Name));

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor([FromRoute]Guid id)
    {

        var doctor = (await GetDoctor(id))!;
        doctor.Deactivate();
        await _persistencia.UpdateDoctorAsync(doctor);
        return NoContent();

    }

    private async Task<Doctor?> GetDoctor(Guid id)
    {

        return await _persistencia.GetDoctorByIdAsync(id) ?? 
            throw new EntityNotFoundException("Medico no encontrado");

    }

}
