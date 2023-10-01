using Foody.Application.Services.ProductServices.Dtos;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class InfoProductCartDto : ProductResponseDto
    {
        public int Quantity { get; set; }
    }
}
