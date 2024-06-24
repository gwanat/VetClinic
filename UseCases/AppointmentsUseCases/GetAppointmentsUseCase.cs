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
        public class GetAppointmentsUseCase
        {
            private readonly IAppointmentsRepository appointmentsRepository;

            public GetAppointmentsUseCase(IAppointmentsRepository appointmentsRepository)
            {
                this.appointmentsRepository = appointmentsRepository;
            }

            public IEnumerable<Appointment> Execute(bool loadRelated = false)
            {
                return appointmentsRepository.GetAppointments(loadRelated);
            }
        }
    }
}
