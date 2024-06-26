using Xunit;
using Moq;
using CoreBusiness;
using UseCases.AppointmentsUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.Tests.AppointmentsUseCases
{
    public class AddAppointmentUseCaseTests
    {
        [Fact]
        public void Execute_Should_Add_Appointment()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var addAppointmentUseCase = new AddAppointmentUseCase(mockAppointmentsRepository.Object);
            var appointment = new Appointment
            {
                AppointmentId = 1,
                StartTime = DateTime.Today.AddHours(9),
                EndTime = DateTime.Today.AddHours(10),
                DoctorId = 1,
                PatientId = 1,
                RoomId = 1
            };

            addAppointmentUseCase.Execute(appointment);

            mockAppointmentsRepository.Verify(repo => repo.AddAppointment(appointment), Times.Once);
        }

        [Fact]
        public void Execute_With_Null_Appointment_Should_Throw_Argument_Null_Exception()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var addAppointmentUseCase = new AddAppointmentUseCase(mockAppointmentsRepository.Object);

            Assert.Throws<ArgumentNullException>(() => addAppointmentUseCase.Execute(null));
        }
    }
}
