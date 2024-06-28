using Xunit;
using Moq;
using UseCases.AppointmentsUseCases;
using UseCases.DataStorePluginInterfaces;
using System;

namespace UseCases.Tests.AppointmentsUseCases
{
    public class DeleteAppointmentUseCaseTests
    {
        [Fact]
        public void Execute_Should_Delete_Appointment()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var deleteAppointmentUseCase = new DeleteAppointmentUseCase(mockAppointmentsRepository.Object);
            int appointmentId = 1;

            deleteAppointmentUseCase.Execute(appointmentId);

            mockAppointmentsRepository.Verify(repo => repo.DeleteAppointment(appointmentId), Times.Once);
        }
    }
}