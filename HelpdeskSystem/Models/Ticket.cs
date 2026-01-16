using System.ComponentModel.DataAnnotations;

namespace HelpdeskSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Open";

        // DATA-LEVEL AUTHORIZATION (MANDATORY)
        [Required]
        public string? CreatedByUserId { get; set; }

        // (Optional) For display purposes only
        [StringLength(100)]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}