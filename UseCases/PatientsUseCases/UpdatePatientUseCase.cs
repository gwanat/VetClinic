using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.PatientsUseCases
{
    public class UpdatePatientUseCase
    {
        private readonly IPatientsRepository patientsRepository;

        public UpdatePatientUseCase(IPatientsRepository patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public void Execute(int patientId, Patient patient)
        {
            if (patient == null)
                throw new ArgumentNullException(nameof(patient));

            patientsRepository.UpdatePatient(patientId, patient);
        }
    }
}
