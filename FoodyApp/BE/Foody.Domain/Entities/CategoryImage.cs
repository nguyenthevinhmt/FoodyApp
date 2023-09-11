using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class CategoryImage : BaseEntity<int>
    {
        public string Description { get; set; }
        public string CategoryImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; }

    }
}
