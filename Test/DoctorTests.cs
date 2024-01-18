using Mesi.Models;
using Mesi.Models.DTOs;
using Mesi.Repository;
using Moq;

namespace Mesi.Test;


public class DoctorTests
{
    [Fact]
    public void GetAppointments_ReturnsCorrectAppointments()
    {
        // Arrange
        var doctorId = 1;
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act
        var result = appointmentService.GetAppointments(doctorId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<AppointmentDTO>>(result);
        // Add more assertions as needed
    }

    [Fact]
    public void ChangeAppointment_UpdatesAppointmentDate()
    {
        // Arrange
        var appointmentId = 1;
        var newDate = DateTime.Now.AddDays(1);
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act
        appointmentService.ChangeAppointment(appointmentId, newDate);

        // Assert
        var updatedAppointment = dbContextMock.Object.Appointments.SingleOrDefault(x => x.Id == appointmentId);
        Assert.NotNull(updatedAppointment);
        Assert.Equal(newDate.Date, updatedAppointment.Date.Date);
        // Add more assertions as needed
    }

    [Fact]
    public void ChangeAppointment_ThrowsExceptionIfInvalidDate()
    {
        // Arrange
        var appointmentId = 1;
        var newDate = DateTime.Now.AddDays(-1);
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => appointmentService.ChangeAppointment(appointmentId, newDate));
    }

    [Fact]
    public void DeleteAppointment_RemovesAppointment()
    {
        // Arrange
        var appointmentId = 1;
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act
        appointmentService.DeleteAppointment(appointmentId);

        // Assert
        var deletedAppointment = dbContextMock.Object.Appointments.SingleOrDefault(x => x.Id == appointmentId);
        Assert.Null(deletedAppointment);
        // Add more assertions as needed
    }

    [Fact]
    public void DeleteAppointment_ThrowsExceptionIfAppointmentNotFound()
    {
        // Arrange
        var invalidAppointmentId = 999;
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => appointmentService.DeleteAppointment(invalidAppointmentId));
    }

    [Fact]
    public void RegisterDoctor_AddsDoctorToDatabase()
    {
        // Arrange
        var doctorDTO = new DoctorDTO(1, "John Doe", "Cardiology", 9, 17);
        var dbContextMock = CreateMockDbContext();
        var appointmentService = new DoctorRepository(dbContextMock.Object);

        // Act
        appointmentService.RegisterDoctor(doctorDTO);

        // Assert
        var addedDoctor = dbContextMock.Object.Doctors.SingleOrDefault(x => x.Name == doctorDTO.Name);
        Assert.NotNull(addedDoctor);
        Assert.Equal(doctorDTO.Department, addedDoctor.Department);
        // Add more assertions as needed
    }

    private Mock<ModelDbContext> CreateMockDbContext()
    {
        var appointments = new List<Appointment>
        {
            new Appointment
            {
                Id = 1,
                Date = new Dates {Id = 1, Date = DateTime.Now.AddDays(1) },
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