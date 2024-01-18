using Mesi.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Mesi.Test;

public static class DbSetExtensions
{
    public static Mock<DbSet<T>> BuildMockDbSet<T>(this IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
        return mockSet;
    }

    public static Mock<ModelDbContext> CreateMockDbContext()
    {
        var appointments = new List<Appointment>
        {
            new Appointment
            {
                Id = 1,
                Date = new Dates {Id = 1, Date = DateTime.Now.AddDays(-2) },
                Doctor = new Doctor ("Dr Smith", "Cardiology", 9, 17),
                Patient = new Patient ("John Doe" )
            },
            // Add more dummy appointments as needed
        };

        var doctors = new List<Doctor>
        {
            new Doctor ("Dr Smith", "Cardiology", 9, 17),
            // Add more dummy doctors as needed
        };

        var patients = new List<Patient>
        {
            new Patient ("John Doe" ),
            // Add more dummy patients as needed
        };

        var dbContextMock = new Mock<ModelDbContext>();

        var appointmentsDbSet = appointments.AsQueryable().BuildMockDbSet();
        var doctorsDbSet = doctors.AsQueryable().BuildMockDbSet();
        var patientsDbSet = patients.AsQueryable().BuildMockDbSet();

        dbContextMock.Setup(x => x.Appointments).Returns(appointmentsDbSet.Object);
        dbContextMock.Setup(x => x.Doctors).Returns(doctorsDbSet.Object);
        dbContextMock.Setup(x => x.Patients).Returns(patientsDbSet.Object);


        return dbContextMock;
    }
}
