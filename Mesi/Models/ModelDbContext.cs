using Microsoft.EntityFrameworkCore;

namespace Mesi.Models;
public class ModelDbContext : DbContext
{
     public ModelDbContext(DbContextOptions<ModelDbContext> options)
        : base(options)
    {
    }
    public ModelDbContext() : base()
    {
    }

    public virtual DbSet<Dates> Dates { get; set; }
    public virtual DbSet<Doctor> Doctors { get; set; }
    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<Appointment> Appointments { get; set; }
}