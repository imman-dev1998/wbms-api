namespace wbms_api.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal RatePerCubicMeter { get; set; }
        public decimal MinimumCharge { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ActiveConsumerCount { get; set; }
    }
}
