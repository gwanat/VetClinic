using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.AppointmentsUseCases
{
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
}
