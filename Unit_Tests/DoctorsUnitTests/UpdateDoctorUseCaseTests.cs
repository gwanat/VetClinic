using Xunit;
using Moq;
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;
using UseCases.DoctorsUseCases;
using System;

namespace UseCases.Tests.DoctorsUseCases
{
    public class UpdateDoctorUseCaseTests
    {
        [Fact]
        public void Execute_Should_Update_Doctor_Successfully()
        {
            int doctorId = 1;
            var updatedDoctor = new Doctor
            {
                DoctorId = doctorId,
                Name = "Updated Jan Kowalski",
                Specialty = "Neurology"
            };

            var mockDoctorsRepository = new Mock<IDoctorsRepository>();
            var updateDoctorUseCase = new UpdateDoctorUseCase(mockDoctorsRepository.Object);

            updateDoctorUseCase.Execute(doctorId, updatedDoctor);

            mockDoctorsRepository.Verify(repo => repo.UpdateDoctor(doctorId, updatedDoctor), Times.Once);
        }
    }
}
