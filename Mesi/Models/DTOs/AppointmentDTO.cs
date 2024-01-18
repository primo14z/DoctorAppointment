namespace Mesi.Models.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = string.Empty;

        public AppointmentDTO(int id, DateTime date, int doctorId, int patientId, string patientName)
        {
            Id = id;
            Date = date;
            DoctorId = doctorId;
            PatientId = patientId;
            PatientName = patientName;
        }
    }
}
