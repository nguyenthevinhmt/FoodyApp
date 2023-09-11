using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Cart : BaseEntity<int>
    {
        public int UserId { get; set; }
        public IEnumerable<ProductCart> ProductCarts { get; set; }
        public IEnumerable<Product> Products { get; set; }

    }
}
