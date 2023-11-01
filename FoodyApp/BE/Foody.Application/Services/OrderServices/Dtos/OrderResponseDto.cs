using Foody.Application.Services.ProductServices.Dtos;
using Foody.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public double TotalAmount { get; set; }
        public List<InfoProductCartDto> Products { get; set; }
        public UserAddressDto UserAddress { get; set; }
        public int PaymentMethod { get; set; }
        public bool IsPaid { get; set; }
    }
}
