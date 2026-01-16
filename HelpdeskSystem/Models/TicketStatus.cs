using System.ComponentModel.DataAnnotations;

namespace HelpdeskSystem.Models
{
    public class TicketStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}