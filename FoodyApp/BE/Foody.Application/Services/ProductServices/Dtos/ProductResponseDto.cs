namespace Foody.Application.Services.ProductServices.Dtos
{
    public class ProductResponseDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal ActualPrice { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
