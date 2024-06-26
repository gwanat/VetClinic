using Xunit;
using Moq;
using CoreBusiness;
using UseCases.DoctorsUseCases;
using UseCases.DataStorePluginInterfaces;
using System;

namespace UseCases.Tests.DoctorsUseCases
{
    public class AddDoctorUseCaseTests
    {
        [Fact]
        public void Execute_Should_Add_Doctor()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var addDoctorUseCase = new AddDoctorUseCase(mockDoctorsRepository.Object);
            var doctor = new Doctor
            {
                DoctorId = 1,
                Name = "Jan Kowalski",
                Specialty = "Cardiology"
            };

            addDoctorUseCase.Execute(doctor);

            mockDoctorsRepository.Verify(repo => repo.AddDoctor(doctor), Times.Once);
        }

        [Fact]
        public void Execute_With_Null_Doctor_Should_Throw_Argument_Null_Exception()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var addDoctorUseCase = new AddDoctorUseCase(mockDoctorsRepository.Object);
            Doctor doctor = null;

            Assert.Throws<ArgumentNullException>(() => addDoctorUseCase.Execute(doctor));
        }
    }
}
