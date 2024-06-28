using Xunit;
using Moq;
using System.Collections.Generic;
using UseCases.PatientsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.PatientsUseCases
{
    public class GetAllPatientsUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_All_Patients()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var patientsList = new List<Patient>
            {
                new Patient { PatientId = 1, DoctorId = 5, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" },
                new Patient { PatientId = 2, DoctorId = 1, PatientName = "Max", Type = "Dog", Breed = "German Shepherd", Age = 5, OwnerInfo = "Jane Smith", Diagnosis = "Skin allergy, medication prescribed" },
            };

            mockPatientsRepository
                .Setup(repo => repo.GetPatients(It.IsAny<bool>()))
                .Returns(patientsList);

            var getAllPatientsUseCase = new GetAllPatientsUseCase(mockPatientsRepository.Object);

            var result = getAllPatientsUseCase.Execute();

            mockPatientsRepository.Verify(repo => repo.GetPatients(false), Times.Once);
            Assert.Equal(patientsList, result);
        }
    }
}
