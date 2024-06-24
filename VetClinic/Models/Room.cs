using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models
{
    public class Room
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room number is required")]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required(ErrorMessage ="Room type is required")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Status")]
        public bool IsOccupied { get; set; }


        public string? Description { get; set; } = string.Empty;

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
