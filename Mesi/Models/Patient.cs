using Mesi.Helpers;

namespace Mesi.Models;

public class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Patient(string name)
    {
        Name = NullCheck.CheckNotNull(name, nameof(name));
    }

    public Patient()
    {
        
    }
}