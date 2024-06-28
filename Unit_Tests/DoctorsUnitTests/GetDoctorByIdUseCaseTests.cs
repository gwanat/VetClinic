using Xunit;
using Moq;
using CoreBusiness;
using UseCases.DoctorsUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.Tests.DoctorsUseCases
{
    public class GetDoctorByIdUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_Doctor()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var getDoctorByIdUseCase = new GetDoctorByIdUseCase(mockDoctorsRepository.Object);
            int doctorId = 1;
            var expectedDoctor = new Doctor
            {
                DoctorId = doctorId,
                Name = "Jan Kowalski",
                Specialty = "Cardiology"
            };

            mockDoctorsRepository.Setup(repo => repo.GetDoctorById(doctorId)).Returns(expectedDoctor);

            var result = getDoctorByIdUseCase.Execute(doctorId);

            Assert.Equal(expectedDoctor, result);
            mockDoctorsRepository.Verify(repo => repo.GetDoctorById(doctorId), Times.Once);
        }

        [Fact]
        public void Execute_With_Invalid_DoctorId_Should_Return_Null()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var getDoctorByIdUseCase = new GetDoctorByIdUseCase(mockDoctorsRepository.Object);
            int invalidDoctorId = -1;

            mockDoctorsRepository.Setup(repo => repo.GetDoctorById(invalidDoctorId)).Returns((Doctor)null);

            var result = getDoctorByIdUseCase.Execute(invalidDoctorId);

            Assert.Null(result);
            mockDoctorsRepository.Verify(repo => repo.GetDoctorById(invalidDoctorId), Times.Once);
        }
    }
}
