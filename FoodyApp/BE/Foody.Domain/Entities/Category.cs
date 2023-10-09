using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Category : BaseEntity<int>, ICreated, IUpdated, ISoftDeleted
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdateBy { get; set; }
    }
}
