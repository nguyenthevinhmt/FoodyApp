using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class ProductImage : BaseEntity<int>
    {
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public long FileSize { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; }
    }
}
