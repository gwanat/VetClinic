using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IPatientsRepository
    {
        void AddPatient(Patient patient);
        void DeletePatient(int patientId);
        Patient? GetPatientById(int patientId, bool loadDoctor);
        List<Patient> GetPatients(bool loadDoctor);
        void UpdatePatient(int patientId, Patient patient);
    }
}