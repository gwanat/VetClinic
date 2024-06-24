using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.DoctorsUseCases
{
    namespace UseCases.DoctorsUseCases
    {
        public class DeleteDoctorUseCase
        {
            private readonly IDoctorsRepository doctorsRepository;

            public DeleteDoctorUseCase(IDoctorsRepository doctorsRepository)
            {
                this.doctorsRepository = doctorsRepository;
            }

            public void Execute(int doctorId)
            {
                doctorsRepository.DeleteDoctor(doctorId);
            }
        }
    }
}
