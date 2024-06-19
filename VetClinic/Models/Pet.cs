using System.ComponentModel.DataAnnotations;

namespace VetClinic.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
