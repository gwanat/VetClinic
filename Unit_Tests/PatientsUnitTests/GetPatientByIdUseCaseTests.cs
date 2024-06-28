using Xunit;
using Moq;
using UseCases.PatientsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.PatientsUseCases
{
    public class GetPatientByIdUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_Patient_By_Id()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var patientId = 1;
            var patient = new Patient { PatientId = 1, DoctorId = 5, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" };

            mockPatientsRepository
                .Setup(repo => repo.GetPatientById(patientId, It.IsAny<bool>()))
                .Returns(patient);

            var getPatientByIdUseCase = new GetPatientByIdUseCase(mockPatientsRepository.Object);

            var result = getPatientByIdUseCase.Execute(patientId);

            mockPatientsRepository.Verify(repo => repo.GetPatientById(patientId, false), Times.Once);
            Assert.Equal(patient, result);
        }

        [Fact]
        public void Execute_Should_Return_Null_If_Patient_Not_Found()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var patientId = 1;

            mockPatientsRepository
                .Setup(repo => repo.GetPatientById(patientId, It.IsAny<bool>()))
                .Returns((Patient)null);

            var getPatientByIdUseCase = new GetPatientByIdUseCase(mockPatientsRepository.Object);

            var result = getPatientByIdUseCase.Execute(patientId);

            mockPatientsRepository.Verify(repo => repo.GetPatientById(patientId, false), Times.Once);
            Assert.Null(result);
        }
    }
}
