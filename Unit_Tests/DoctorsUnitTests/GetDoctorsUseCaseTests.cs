using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using UseCases.DoctorsUseCases;

namespace UseCases.Tests.DoctorsUseCases
{
    public class GetDoctorsUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_Doctors()
        {
            var expectedDoctors = new List<Doctor>
            {
                new Doctor { DoctorId = 1, Name = "Doctor A", Specialty = "Cardiology" },
                new Doctor { DoctorId = 2, Name = "Doctor B", Specialty = "Pediatrics" }
            };

            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            mockDoctorsRepository.Setup(repo => repo.GetDoctors()).Returns(expectedDoctors);

            var getDoctorsUseCase = new GetDoctorsUseCase(mockDoctorsRepository.Object);

            var result = getDoctorsUseCase.Execute();

            Assert.Equal(expectedDoctors, result);
            mockDoctorsRepository.Verify(repo => repo.GetDoctors(), Times.Once);
        }

        [Fact]
        public void Execute_With_Empty_Collection_Should_Return_Empty()
        {
            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            mockDoctorsRepository.Setup(repo => repo.GetDoctors()).Returns(new List<Doctor>());

            var getDoctorsUseCase = new GetDoctorsUseCase(mockDoctorsRepository.Object);

            var result = getDoctorsUseCase.Execute();

            Assert.Empty(result);
            mockDoctorsRepository.Verify(repo => repo.GetDoctors(), Times.Once);
        }
    }
}
