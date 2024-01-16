using Mesi.Models.DTOs;

namespace Mesi.Repository;

public interface IDoctorRepository
{
    public List<AppointmentDTO> GetAppointments(int doctorId);
    public void ChangeAppointment(int id, DateTime newDate);
    public void DeleteAppointment(int id);
}