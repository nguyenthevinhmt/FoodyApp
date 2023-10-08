using Foody.Domain.Entities;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public int ProductCartId { get; set; }
        public int PaymentMethod { get; set; }
        public List<OrderDetail> orderDetails { get; set; }
    }
}
