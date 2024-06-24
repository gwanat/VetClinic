using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.AppointmentsUseCases
{
    public class UpdateAppointmentUseCase
    {
        private readonly IAppointmentsRepository appointmentsRepository;

        public UpdateAppointmentUseCase(IAppointmentsRepository appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        public void Execute(int appointmentId, Appointment appointment)
        {
            appointmentsRepository.UpdateAppointment(appointmentId, appointment);
        }
    }
}
