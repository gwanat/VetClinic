using CoreBusiness;
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
        public class GetAppointmentByIdUseCase
        {
            private readonly IAppointmentsRepository appointmentsRepository;

            public GetAppointmentByIdUseCase(IAppointmentsRepository appointmentsRepository)
            {
                this.appointmentsRepository = appointmentsRepository;
            }

            public Appointment Execute(int appointmentId, bool loadRelated = false)
            {
                return appointmentsRepository.GetAppointmentById(appointmentId, loadRelated);
            }
        }
    }
}
