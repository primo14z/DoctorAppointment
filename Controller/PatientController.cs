using Microsoft.AspNetCore.Mvc;
using Mesi.Repository;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetDoctorList()
    {
        return Ok(await _patientRepository.GetDoctorList());
    }

    [HttpPost]
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

    [HttpGet]
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
}

