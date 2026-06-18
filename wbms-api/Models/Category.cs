using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wbms_api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required, Column(TypeName = "decimal(10,2)")]
        public decimal RatePerCubicMeter { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumCharge { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public ICollection<Consumer> Consumers { get; set; } = new List<Consumer>();
    }
}
