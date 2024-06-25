using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoreBusiness
{
    public class Doctor
    {
        public int DoctorId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Doctor's name is required")]
        public string Name { get; set; }

        [DisplayName("Specialty")]
        [Required(ErrorMessage = "Doctor's specialty is required")]
        public string? Specialty { get; set; }

        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
