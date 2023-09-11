using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Promotion : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Product> Products { get; }
    }
}
