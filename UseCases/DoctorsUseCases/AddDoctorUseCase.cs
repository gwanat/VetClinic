using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.DoctorsUseCases
{
    public class AddDoctorUseCase
    {
        private readonly IDoctorsRepository doctorsRepository;

        public AddDoctorUseCase(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public void Execute(Doctor doctor)
        {
            doctorsRepository.AddDoctor(doctor);
        }
    }
}
