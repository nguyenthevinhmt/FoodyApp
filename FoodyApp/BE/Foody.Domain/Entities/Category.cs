using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryImageUrl { get; set; }
        public List<CategoryImage> CategoryImage { get; set; }
        public int ProductId { get; set; }
        public Product Products { get; }
    }
}
