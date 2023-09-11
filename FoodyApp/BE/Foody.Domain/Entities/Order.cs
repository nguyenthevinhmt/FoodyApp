using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Order : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int ProductCartId { get; set; }
        public ProductCart ProductCart { get; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
