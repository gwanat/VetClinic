using Xunit;
using Moq;
using CoreBusiness;
using UseCases.AppointmentsUseCases;
using UseCases.DataStorePluginInterfaces;
using System.Collections.Generic;
using UseCases.AppointmentsUseCases.UseCases.AppointmentsUseCases;

namespace UseCases.Tests.AppointmentsUseCases
{
    public class GetAppointmentsUseCaseTests
    {
        [Fact]
        public void Execute_ShouldReturnAllAppointments()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var appointments = new List<Appointment>
            {
                new Appointment {AppointmentId = 1, StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(10), DoctorId = 1, PatientId = 1, RoomId = 1},
                new Appointment {AppointmentId = 2, StartTime = DateTime.Today.AddDays(1).AddHours(14), EndTime = DateTime.Today.AddDays(1).AddHours(15), DoctorId = 2, PatientId = 2, RoomId = 2}
            };
            mockAppointmentsRepository.Setup(repo => repo.GetAppointments(false)).Returns(appointments);

            var getAppointmentsUseCase = new GetAppointmentsUseCase(mockAppointmentsRepository.Object);

            var result = getAppointmentsUseCase.Execute();

            Assert.Equal(appointments, result);
        }
    }
}
