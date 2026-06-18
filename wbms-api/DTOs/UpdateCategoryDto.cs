using System.ComponentModel.DataAnnotations;

namespace wbms_api.DTOs
{
    public class UpdateCategoryDto
    {
        [MaxLength(100)]
        public string? Name { get; set; }

        [MaxLength(255)]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Rate must be greater than 0")]
        public decimal? RatePerCubicMeter { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? MinimumCharge { get; set; }

        public bool? IsActive { get; set; }
    }
}
