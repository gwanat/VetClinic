using Xunit;
using Moq;
using UseCases.PatientsUseCases;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.Tests.PatientsUseCases
{
    public class UpdatePatientUseCaseTests
    {
        [Fact]
        public void Execute_Should_Update_Patient()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var patientId = 1;
            var patient = new Patient { PatientId = 1, DoctorId = 5, PatientName = "Bella", Type = "Dog", Breed = "Labrador", Age = 3, OwnerInfo = "John Doe", Diagnosis = "Healthy, routine check-up" };

            var updatePatientUseCase = new UpdatePatientUseCase(mockPatientsRepository.Object);

            updatePatientUseCase.Execute(patientId, patient);

            mockPatientsRepository.Verify(repo => repo.UpdatePatient(patientId, patient), Times.Once);
        }

        [Fact]
        public void Execute_Should_Not_Call_Repository_If_Patient_Is_Null()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var patientId = 1;
            Patient patient = null;

            var updatePatientUseCase = new UpdatePatientUseCase(mockPatientsRepository.Object);

            Assert.Throws<ArgumentNullException>(() => updatePatientUseCase.Execute(patientId, patient));
            mockPatientsRepository.Verify(repo => repo.UpdatePatient(It.IsAny<int>(), It.IsAny<Patient>()), Times.Never);
        }
    }
}
