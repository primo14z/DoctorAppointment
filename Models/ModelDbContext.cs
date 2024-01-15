using Microsoft.EntityFrameworkCore;

namespace Mesi.Models;
public class ModelDbContext : DbContext
{
     public ModelDbContext(DbContextOptions<ModelDbContext> options)
        : base(options)
    {
    }

    public DbSet<Dates> Dates { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
}