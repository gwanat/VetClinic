using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required(ErrorMessage = "Doctor's name is required")]
        [Display(Name = "Doctor")]
        public int? DoctorId { get; set; }

        [Display(Name = "Pet's Name")]
        [Required(ErrorMessage = "Pet's name is required")]
        public string PatientName { get; set; } = string.Empty;

        [Display(Name = "Species")]
        [Required(ErrorMessage = "Pet's species is required")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet's breed is required")]
        public string Breed { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet's age is required")]
        public int Age { get; set; }

        [Display(Name = "Owner's Name")]
        [Required(ErrorMessage = "Owner's name is required")]
        public string OwnerInfo { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Diagnosis is required")]
        public string Diagnosis { get; set; } = string.Empty;

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
