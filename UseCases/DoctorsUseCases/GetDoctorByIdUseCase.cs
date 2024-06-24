using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.DoctorsUseCases
{
    public class GetDoctorByIdUseCase
    {
        private readonly IDoctorsRepository doctorsRepository;

        public GetDoctorByIdUseCase(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public Doctor Execute(int doctorId)
        {
            return doctorsRepository.GetDoctorById(doctorId);
        }
    }
}
