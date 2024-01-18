using Mesi.Models;
using Mesi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mesi.Repository;
public class PatientRepository : IPatientRepository
{
    private ModelDbContext _dbContext;

    public PatientRepository(ModelDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DoctorDTO>> GetDoctorList()
    {
        var result = new List<DoctorDTO>();

        foreach (var item in _dbContext.Doctors)
        {
            result.Add(new DoctorDTO(item.Id, item.Name, item.Department, item.WorkStart, item.WorkEnd));
        }

        return result;
    }

    public void RegisterPatient(string name)
    {
        _dbContext.Patients.Add(new Patient(name));
        _dbContext.SaveChanges();
    }

    public List<PatientAppointmentDTO> GetDoctorAppointments(int patientId, int doctorId, DateTime startDate, DateTime endDate)
    {
        var result = new List<PatientAppointmentDTO>();

        var appointments = _dbContext
            .Appointments
            .Include(x => x.Doctor)
            .Include(x => x.Patient)
            .Include(x => x.Date)
            .Where(x => x.Doctor.Id == doctorId && x.Date.Date >= startDate && x.Date.Date <= endDate)
            .Select(x => x).ToList();

        appointments.ForEach(x => result.Add(
            new PatientAppointmentDTO(
                new DoctorDTO(x.Doctor.Id, x.Doctor.Name, x.Doctor.Department, x.Doctor.WorkStart, x.Doctor.WorkEnd),
                x.Id,
                x.Date.Date,
                x.Patient.Id == patientId)));

        return result;
    }

    public void SetAppointment(AppointmentDTO appointmentDTO)
    {
        try
        {
            var date = _dbContext.Dates.Single(x => x.Date == appointmentDTO.Date);
            var doctor = _dbContext.Doctors.Single(x => x.Id == appointmentDTO.DoctorId);
            var patient = _dbContext.Patients.Single(x => x.Id == appointmentDTO.PatientId);

            _dbContext.Appointments.Add(new Appointment
            {
                Date = date,
                Doctor = doctor,
                Patient = patient
            });

            _dbContext.SaveChanges();
        }
        catch
        {
            throw;
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
}