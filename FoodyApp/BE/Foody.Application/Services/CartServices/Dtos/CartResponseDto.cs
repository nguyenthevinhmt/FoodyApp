using Foody.Application.Services.OrderServices.Dtos;

namespace Foody.Application.Services.CartServices.Dtos
{
    public class CartResponseDto
    {
        public int CartId { get; set; }
        public double TotalPrice { get; set; }
        public List<InfoProductCart1Dto> Products { get; set; }
    }
}
