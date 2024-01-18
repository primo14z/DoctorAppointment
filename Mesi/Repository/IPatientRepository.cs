using Mesi.Models.DTOs;

namespace Mesi.Repository;
public interface IPatientRepository
{
    public Task<List<DoctorDTO>> GetDoctorList();
    public void RegisterPatient(string name);
    public List<PatientAppointmentDTO> GetDoctorAppointments(int patientId, int doctorId, DateTime startDate, DateTime endDate);
    public void SetAppointment(AppointmentDTO appointmentDTO);
    public void DeleteAppointment(int  id);
}