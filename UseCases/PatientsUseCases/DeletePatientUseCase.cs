using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.PatientsUseCases
{
    public class DeletePatientUseCase
    {
        private readonly IPatientsRepository patientsRepository;

        public DeletePatientUseCase(IPatientsRepository patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public void Execute(int patientId)
        {
            patientsRepository.DeletePatient(patientId);
        }
    }
}
