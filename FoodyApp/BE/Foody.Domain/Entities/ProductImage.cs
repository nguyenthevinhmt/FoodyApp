using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class ProductImage : BaseEntity<int>, ICreated, IUpdated, ISoftDeleted
    {
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public long FileSize { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; }
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
