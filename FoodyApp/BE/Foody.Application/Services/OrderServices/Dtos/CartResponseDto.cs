namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CartResponseDto
    {
        public int OrderId { get; set; }
        public double TotalPrice { get; set; }
        public List<InfoProductCartDto> Products { get; set; }
    }
}
