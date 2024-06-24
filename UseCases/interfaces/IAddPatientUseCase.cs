using CoreBusiness;

namespace UseCases.PatientsUseCases
{
    public interface IAddPatientUseCase
    {
        void Execute(Patient patient);
    }
}