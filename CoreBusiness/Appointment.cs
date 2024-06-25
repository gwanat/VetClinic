using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreBusiness
{
    public class Appointment
    {   
        
        public int AppointmentId { get; set; }

        [DisplayName("Start time")]
        [Required]
        public DateTime StartTime { get; set; }

        [DisplayName("End time")]
        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }

        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [Required]
        public int RoomId { get; set; }

        [DisplayName("Room number")]
        [ForeignKey("RoomId")]
        public Room? Room { get; set; }

        [DisplayName("Owner of the pet")]
        [NotMapped]
        public string ClientName => Patient?.OwnerInfo ?? string.Empty;

        [NotMapped]
        [DisplayName("Patient Name")]
        public string? PatientName { get; set; }
    }
}
