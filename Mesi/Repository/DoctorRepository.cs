using Mesi.Models;
using Mesi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mesi.Repository;

public class DoctorRepository : IDoctorRepository
{
    private ModelDbContext _dbContext;

    public DoctorRepository(ModelDbContext modelDbContext)
    {
        _dbContext = modelDbContext;
    }

    public List<AppointmentDTO> GetAppointments(int doctorId)
    {
        var result = new List<AppointmentDTO>();

        var appointments = _dbContext
            .Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Include(x => x.Date)
            .Where(x => x.Doctor.Id == doctorId 
                && x.Date.Date >= DateTime.Now
                && x.Date.Date.Hour >= x.Doctor.WorkStart
                && x.Date.Date.Hour <= x.Doctor.WorkEnd)
            .Select(x => x).ToList();

        if (appointments.Any())
            appointments.ForEach(x => result.Add(new AppointmentDTO(x.Id, x.Date.Date, x.Doctor.Id, x.Patient.Id, x.Patient.Name)));

        return result;
    }

    public void ChangeAppointment(int id, DateTime newDate)
    {
        var appointment = _dbContext
            .Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Include(x => x.Date)
            .Where(x => x.Id == id).Single();

        if (newDate > appointment.Date.Date 
            && appointment.Doctor.WorkStart <= newDate.Hour 
            && appointment.Doctor.WorkEnd >= newDate.Hour)
        {
            appointment.Date.Date = newDate;
            _dbContext.SaveChanges();
        }
        else
        {
            throw new Exception("Date cannot be smaller than original date.");
        }
    }

    public void DeleteAppointment(int id)
    {
        try
        {
            var appointment = _dbContext.Appointments.Single(x => x.Id == id);

            _dbContext.Appointments.Remove(appointment);

            _dbContext.SaveChanges();
        }
        catch
        {
            throw new Exception("Appointment doesn't exist.");
        }
    }

    public void RegisterDoctor(DoctorDTO doctorDTO)
    {
        try
        {
            _dbContext.Doctors.Add(new Doctor(doctorDTO.Name, doctorDTO.Department, doctorDTO.WorkStart, doctorDTO.WorkEnd));

            _dbContext.SaveChanges();
        }
        catch
        {
            throw;
        }
    }
}
