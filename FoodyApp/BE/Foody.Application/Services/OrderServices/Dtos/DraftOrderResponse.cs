using Foody.Domain.Entities;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class DraftOrderResponse
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public List<Product> Product { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
