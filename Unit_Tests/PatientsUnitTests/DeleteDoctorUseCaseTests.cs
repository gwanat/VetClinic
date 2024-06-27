using Xunit;
using Moq;
using UseCases.PatientsUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.Tests.PatientsUseCases
{
    public class DeletePatientUseCaseTests
    {
        [Fact]
        public void Execute_Should_Delete_Patient()
        {
            var mockPatientsRepository = new Mock<IPatientsRepository>();
            var deletePatientUseCase = new DeletePatientUseCase(mockPatientsRepository.Object);
            int patientId = 1;

            deletePatientUseCase.Execute(patientId);

            mockPatientsRepository.Verify(repo => repo.DeletePatient(patientId), Times.Once);
        }
    }
}