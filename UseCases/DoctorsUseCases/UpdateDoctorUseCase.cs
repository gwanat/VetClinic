using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.DoctorsUseCases
{
    public class UpdateDoctorUseCase
    {
        private readonly IDoctorsRepository doctorsRepository;

        public UpdateDoctorUseCase(IDoctorsRepository doctorsRepository)
        {
            this.doctorsRepository = doctorsRepository;
        }

        public void Execute(int doctorId, Doctor doctor)
        {
            doctorsRepository.UpdateDoctor(doctorId, doctor);
        }
    }
}
