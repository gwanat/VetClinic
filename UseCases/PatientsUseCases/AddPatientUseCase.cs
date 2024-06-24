using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using CoreBusiness;

namespace UseCases.PatientsUseCases
{
    public class AddPatientUseCase : IAddPatientUseCase
    {
        private readonly IPatientsRepository patientsRepository;

        public AddPatientUseCase(IPatientsRepository patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public void Execute(Patient patient)
        {
            patientsRepository.AddPatient(patient);
        }
    }
}
