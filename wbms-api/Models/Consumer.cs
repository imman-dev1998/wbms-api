namespace wbms_api.Models
{
    public class Consumer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public Category Category { get; set; } = null!;
    }
}
