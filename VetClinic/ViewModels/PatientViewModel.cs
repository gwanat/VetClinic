using VetClinic.Models;

namespace VetClinic.ViewModels
{
    public class PatientViewModel
    {
        public IEnumerable<Pet> Pets { get; set; } = new List<Pet>();
        public Patient Patient { get; set; } = new Patient();
    }
}
