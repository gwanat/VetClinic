using Xunit;
using Moq;
using CoreBusiness;
using UseCases.AppointmentsUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.Tests.AppointmentsUseCases
{
    public class UpdateAppointmentUseCaseTests
    {
        [Fact]
        public void Execute_Should_Update_Appointment()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var updateAppointmentUseCase = new UpdateAppointmentUseCase(mockAppointmentsRepository.Object);
            int appointmentId = 1;
            var appointment = new Appointment
            {
                AppointmentId = 1, StartTime = DateTime.Today.AddHours(9), EndTime = DateTime.Today.AddHours(10), DoctorId = 1, PatientId = 1, RoomId = 1
            };

            updateAppointmentUseCase.Execute(appointmentId, appointment);

            mockAppointmentsRepository.Verify(repo => repo.UpdateAppointment(appointmentId, appointment), Times.Once);
        }

        [Fact]
        public void Execute_With_Null_Appointment_Should_Throw_Argument_Null_Exception()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var updateAppointmentUseCase = new UpdateAppointmentUseCase(mockAppointmentsRepository.Object);
            int appointmentId = 1;
            Appointment appointment = null;

            Assert.Throws<ArgumentNullException>(() => updateAppointmentUseCase.Execute(appointmentId, appointment));
        }
    }
}
