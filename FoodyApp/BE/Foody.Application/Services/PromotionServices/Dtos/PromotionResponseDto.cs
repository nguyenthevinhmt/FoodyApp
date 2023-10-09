namespace Foody.Application.Services.PromotionServices.Dtos
{
    public class PromotionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PromotionCode { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
