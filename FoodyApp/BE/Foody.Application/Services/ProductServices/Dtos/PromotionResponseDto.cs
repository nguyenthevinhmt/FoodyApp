namespace Foody.Application.Services.ProductServices.Dtos
{
    public class PromotionResponseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PromotionCode { get; set; }
        public double DiscountPercent { get; set; }
        public bool IsActive { get; set; }
    }
}
