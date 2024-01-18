using Mesi.Helpers;

namespace Mesi.Models.DTOs
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int WorkStart { get; set; }
        public int WorkEnd { get; set; }

        public DoctorDTO(int id, string name, string department, int workStart, int workEnd)
        {
            Id = id;
            Name = NullCheck.CheckNotNull(name, nameof(name));
            Department = NullCheck.CheckNotNull(department, nameof(department));
            WorkStart = NullCheck.CheckGreaterThanZero(workStart, nameof(workStart));
            WorkEnd = NullCheck.CheckGreaterThanZero(workEnd, nameof(workEnd));
        }
    }
}