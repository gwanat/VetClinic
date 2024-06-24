using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Doctor's name is required")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Specialty")]
        [Required(ErrorMessage = "Doctor's specialty is required")]
        public string? Specialty { get; set; } = string.Empty;

        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
