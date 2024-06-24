using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IDoctorsRepository
    {
        void AddDoctor(Doctor doctor);
        void DeleteDoctor(int doctorId);
        Doctor GetDoctorById(int doctorId);
        IEnumerable<Doctor> GetDoctors();
        void UpdateDoctor(int doctorId, Doctor doctor);
    }
}