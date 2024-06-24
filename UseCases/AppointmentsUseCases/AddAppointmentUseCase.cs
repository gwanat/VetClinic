using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.AppointmentsUseCases
{
    public class AddAppointmentUseCase
    {
        private readonly IAppointmentsRepository appointmentsRepository;

        public AddAppointmentUseCase(IAppointmentsRepository appointmentsRepository)
        {
            this.appointmentsRepository = appointmentsRepository;
        }

        public void Execute(Appointment appointment)
        {
            appointmentsRepository.AddAppointment(appointment);
        }
    }
}
