using Xunit;
using Moq;
using CoreBusiness;
using UseCases.AppointmentsUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.AppointmentsUseCases.UseCases.AppointmentsUseCases;

namespace UseCases.Tests.AppointmentsUseCases
{
    public class GetAppointmentByIdUseCaseTests
    {
        [Fact]
        public void Execute_Should_Return_Appointment()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            var appointment = new Appointment
            {
                AppointmentId = 1,
                StartTime = DateTime.Today.AddHours(9),
                EndTime = DateTime.Today.AddHours(10),
                DoctorId = 1,
                PatientId = 1,
                RoomId = 1
            };
            mockAppointmentsRepository.Setup(repo => repo.GetAppointmentById(1, false)).Returns(appointment);

            var getAppointmentByIdUseCase = new GetAppointmentByIdUseCase(mockAppointmentsRepository.Object);

            var result = getAppointmentByIdUseCase.Execute(1);

            Assert.Equal(appointment, result);
        }

        [Fact]
        public void Execute_With_Invalid_Id_Should_Return_Null()
        {
            var mockAppointmentsRepository = new Mock<IAppointmentsRepository>();
            mockAppointmentsRepository.Setup(repo => repo.GetAppointmentById(It.IsAny<int>(), It.IsAny<bool>())).Returns((Appointment)null);

            var getAppointmentByIdUseCase = new GetAppointmentByIdUseCase(mockAppointmentsRepository.Object);

            var result = getAppointmentByIdUseCase.Execute(-1);

            Assert.Null(result);
        }
    }
}
