using Mesi.Models.DTOs;
using Mesi.Repository;

namespace Mesi.Test;


public class DoctorTests
{
    [Fact]
    public void GetAppointments_ReturnsCorrectAppointments()
    {
        // Arrange
        var doctorId = 1;
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act
        var result = doctorRepository.GetAppointments(doctorId);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<AppointmentDTO>>(result);
    }

    [Fact]
    public void ChangeAppointment_UpdatesAppointmentDate()
    {
        // Arrange
        var appointmentId = 1;
        var newDate = new DateTime(2024, 1, 20, 10, 0, 0);
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act
        doctorRepository.ChangeAppointment(appointmentId, newDate);

        // Assert
        var updatedAppointment = dbContextMock.Object.Appointments.SingleOrDefault(x => x.Id == appointmentId);
        Assert.NotNull(updatedAppointment);
    }

    [Fact]
    public void ChangeAppointment_ThrowsExceptionIfInvalidDate()
    {
        // Arrange
        var appointmentId = 1;
        var newDate = DateTime.Now.AddDays(-2);
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => doctorRepository.ChangeAppointment(appointmentId, newDate));
    }

    [Fact]
    public void DeleteAppointment_RemovesAppointment()
    {
        // Arrange
        var appointmentId = 1;
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act
        doctorRepository.DeleteAppointment(appointmentId);

    }

    [Fact]
    public void DeleteAppointment_ThrowsExceptionIfAppointmentNotFound()
    {
        // Arrange
        var invalidAppointmentId = 999;
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => doctorRepository.DeleteAppointment(invalidAppointmentId));
    }

    [Fact]
    public void RegisterDoctor_AddsDoctorToDatabase()
    {
        // Arrange
        var doctorDTO = new DoctorDTO(1, "Dr Smith", "Cardiology", 9, 17);
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var doctorRepository = new DoctorRepository(dbContextMock.Object);

        // Act
        doctorRepository.RegisterDoctor(doctorDTO);

        // Assert
        var addedDoctor = dbContextMock.Object.Doctors.SingleOrDefault(x => x.Name == doctorDTO.Name);
        Assert.NotNull(addedDoctor);
        Assert.Equal(doctorDTO.Department, addedDoctor.Department);
    } 
}