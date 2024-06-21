using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinic.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [Required]
        [Display(Name = "Doctor")]
        public int? DoctorId { get; set; }

        [Required]
        public string PatientName { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string Breed { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string OwnerInfo { get; set; } = string.Empty;

        public string Diagnosis { get; set; } = string.Empty;

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
