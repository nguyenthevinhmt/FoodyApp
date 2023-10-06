using Foody.Domain.Common;
using Foody.Domain.Constants;

namespace Foody.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int Status { get; set; }
        public int ProductCartId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
