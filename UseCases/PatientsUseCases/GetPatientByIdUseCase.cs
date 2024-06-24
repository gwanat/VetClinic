using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.PatientsUseCases
{
    public class GetPatientByIdUseCase
    {
        private readonly IPatientsRepository patientsRepository;

        public GetPatientByIdUseCase(IPatientsRepository patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public Patient? Execute(int patientId, bool loadDoctor = false)
        {
            return patientsRepository.GetPatientById(patientId, loadDoctor);
        }
    }
}
