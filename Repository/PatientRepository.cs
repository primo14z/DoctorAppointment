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

        await _dbContext.Doctors.ForEachAsync(x => result.Add(new DoctorDTO(x.Id, x.Name, x.Department, x.WorkStart, x.WorkEnd)));

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
}