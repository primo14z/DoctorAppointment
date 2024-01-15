namespace Mesi.Models;

public class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Departament { get; set; }
    public List<Appointment> Appointments { get; set; }
}