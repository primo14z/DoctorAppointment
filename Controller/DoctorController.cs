using Microsoft.AspNetCore.Mvc;
using Mesi.Repository;
using Mesi.Models.DTOs;

namespace Mesi.Controller;

[ApiController]
[Route("api/[controller]")]
public class DoctorController : ControllerBase
{
    private IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
       _doctorRepository = doctorRepository;
    }

    [HttpGet]
    public List<AppointmentDTO> GetAppointments(int id)
    {
        return _doctorRepository.GetAppointments(id);
    }

    [HttpPost]
    public IActionResult ChangeAppointment(int id, DateTime newDate)
    {
        try
        {
            _doctorRepository.ChangeAppointment(id, newDate);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult DeleteAppointment(int id) 
    {
        try
        {
            _doctorRepository.DeleteAppointment(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}