using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Promotion : BaseEntity<int>, ICreated, IUpdated, ISoftDeleted
    {
        public string Name { get; set; }
        public string PromotionCode { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; } = true;
        public IEnumerable<ProductPromotion> ProductPromotions { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
