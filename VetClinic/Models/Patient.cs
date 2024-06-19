using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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
        public string Breed { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        public string OwnerInfo { get; set; } = string.Empty;

        public Doctor? Doctor { get; set; }

    }
}

