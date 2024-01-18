namespace Mesi.Models.DTOs
{
    public class PatientAppointmentDTO
    {
        public DoctorDTO Doctor { get; set; }
        public int AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public bool isPatient { get; set; }

        public PatientAppointmentDTO(DoctorDTO doctor, int appointmentId, DateTime date, bool IsPatient)
        {
            Doctor = doctor;
            AppointmentId = appointmentId;
            Date = date;
            isPatient = IsPatient;
        }
    }
}