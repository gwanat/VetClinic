using System;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.AppointmentsUseCases
{
    public class DeleteAppointmentUseCase
    {
        private readonly IAppointmentsRepository appointmentsRepository;

        public DeleteAppointmentUseCase(IAppointmentsRepository appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        public void Execute(int appointmentId)
        {
            appointmentsRepository.DeleteAppointment(appointmentId);
        }
    }
}
