using Mesi.Models.DTOs;
using Mesi.Repository;
using Mesi.Test;

namespace TestProject;

public class PatientTests
{

    [Fact]
    public async Task GetDoctorList_ReturnsDoctorList()
    {
        // Arrange
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var patientRepository = new PatientRepository(dbContextMock.Object);

        // Act
        var result = await patientRepository.GetDoctorList();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<DoctorDTO>>(result);
        Assert.Equal(1, result.Count); 
    }

    [Fact]
    public void RegisterPatient_AddsPatientToDatabase()
    {
        // Arrange
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var patientRepository = new PatientRepository(dbContextMock.Object);

        // Act
        patientRepository.RegisterPatient("John Doe");

        // Assert
        var addedPatient = dbContextMock.Object.Patients.SingleOrDefault(p => p.Name == "John Doe");
        Assert.NotNull(addedPatient);
    }

    [Fact]
    public void GetDoctorAppointments_ReturnsDoctorAppointments()
    {
        // Arrange
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var patientRepository = new PatientRepository(dbContextMock.Object);
        var patientId = 1;
        var doctorId = 1;
        var startDate = DateTime.Now;
        var endDate = DateTime.Now.AddDays(7);

        // Act
        var result = patientRepository.GetDoctorAppointments(patientId, doctorId, startDate, endDate);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<List<PatientAppointmentDTO>>(result);
        Assert.Equal(0, result.Count); 
    }

    [Fact]
    public void DeleteAppointment_RemovesAppointmentFromDatabase()
    {
        // Arrange
        var dbContextMock = DbSetExtensions.CreateMockDbContext();
        var patientRepository = new PatientRepository(dbContextMock.Object);
        var appointmentIdToRemove = 1;

        // Act
        patientRepository.DeleteAppointment(appointmentIdToRemove);
    }
}
