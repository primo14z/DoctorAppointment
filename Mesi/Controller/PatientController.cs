using Microsoft.AspNetCore.Mvc;
using Mesi.Repository;
using Microsoft.AspNetCore.Authorization;
using Mesi.Models.DTOs;
using Mesi.Models;

namespace Mesi.Controller;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private IPatientRepository _patientRepository;

    public PatientController(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    [HttpGet("GetDoctorList")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetDoctorList()
    {
        return Ok(await _patientRepository.GetDoctorList());
    }

    [HttpPost("RegisterPatient")]
    public IActionResult RegisterPatient(string name)
    {
        try
        {
            _patientRepository.RegisterPatient(name);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("GetDoctorAppointments")]
    [Authorize(Roles = "Patient")]
    public IActionResult GetDoctorAppointments(int patientId, int doctorId, DateTime startDate, DateTime endDate)
    {
        try
        {
            return Ok(_patientRepository.GetDoctorAppointments(patientId, doctorId, startDate, endDate));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("SetAppointment")]
    [Authorize(Roles = "Patient")]
    public IActionResult SetAppointment(AppointmentDTO appointmentDTO)
    {
        try
        {
            _patientRepository.SetAppointment(appointmentDTO);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet("RemoveAppointment")]
    [Authorize(Roles = "Patient")]
    public IActionResult RemoveAppointment(int appointmentId)
    {
        try
        {
            _patientRepository.DeleteAppointment(appointmentId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}

