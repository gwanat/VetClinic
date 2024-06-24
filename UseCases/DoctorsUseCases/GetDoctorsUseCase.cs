using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.DoctorsUseCases
{
    public class GetDoctorsUseCase
    {
        private readonly IDoctorsRepository doctorsRepository;

        public GetDoctorsUseCase(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public IEnumerable<Doctor> Execute()
        {
            return doctorsRepository.GetDoctors();
        }
    }
}
