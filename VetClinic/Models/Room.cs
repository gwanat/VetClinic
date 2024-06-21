using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        public bool IsOccupied { get; set; }

        public string? Description { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
