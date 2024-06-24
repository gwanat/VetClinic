using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.PatientsUseCases
{
    public class GetAllPatientsUseCase
    {
        private readonly IPatientsRepository patientsRepository;

        public GetAllPatientsUseCase(IPatientsRepository patientsRepository)
        {
            this.patientsRepository = patientsRepository;
        }

        public List<Patient> Execute(bool loadDoctor = false)
        {
            return patientsRepository.GetPatients(loadDoctor);
        }
    }
}
