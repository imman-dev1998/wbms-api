using System.ComponentModel.DataAnnotations;

namespace wbms_api.DTOs
{
    public class CreateCategoryDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0")]
        public decimal RatePerCubicMeter { get; set; }

        [Range(0, double.MaxValue)]
        public decimal MinimumCharge { get; set; }
    }
}
