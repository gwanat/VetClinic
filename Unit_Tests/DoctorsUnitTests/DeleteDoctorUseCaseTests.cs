using Xunit;
using Moq;
using UseCases.DoctorsUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.DoctorsUseCases.UseCases.DoctorsUseCases;

namespace UseCases.Tests.DoctorsUseCases
{
    public class DeleteDoctorUseCaseTests
    {
        [Fact]
        public void Execute_Should_Delete_Doctor()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var deleteDoctorUseCase = new DeleteDoctorUseCase(mockDoctorsRepository.Object);
            int doctorId = 1;

            deleteDoctorUseCase.Execute(doctorId);

            mockDoctorsRepository.Verify(repo => repo.DeleteDoctor(doctorId), Times.Once);
        }
    }
}
