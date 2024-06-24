using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IAppointmentsRepository
    {
        void AddAppointment(Appointment appointment);
        void DeleteAppointment(int appointmentId);
        Appointment GetAppointmentById(int appointmentId, bool loadRelated);
        IEnumerable<Appointment> GetAppointments(bool loadRelated);
        void UpdateAppointment(int appointmentId, Appointment appointment);
    }
}