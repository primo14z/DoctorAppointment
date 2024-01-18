using Mesi.Helpers;

namespace Mesi.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public int WorkStart { get; set; }
    public int WorkEnd { get; set; }
    public List<Appointment> Appointments { get; set; } = new List<Appointment>();

    public Doctor(string name, string department, int workStart, int workEnd)
    {
        Name = NullCheck.CheckNotNull(name, nameof(name));
        Department = NullCheck.CheckNotNull(department, nameof(department));
        WorkStart = NullCheck.CheckGreaterThanZero(workStart, nameof(workStart));
        WorkEnd = NullCheck.CheckGreaterThanZero(workEnd, nameof(workEnd));
    }

    public Doctor()
    {
        
    }
}