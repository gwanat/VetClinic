using VetClinic.Models;

namespace VetClinic.ViewModels
{
    public class PatientViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; } = new List<Doctor>();
        public Patient Patient { get; set; } = new Patient();
    }
}
