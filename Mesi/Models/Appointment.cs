namespace Mesi.Models;
public class Appointment
{
    public int Id { get; set; }
    public Dates Date { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }

    public Appointment()
    {
        
    }
}