using Foody.Domain.Common;

namespace Foody.Domain.Entities
{
    public class Order : BaseEntity<int>, ICreated, IUpdated, ISoftDeleted
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int Status { get; set; }
        public int ProductCartId { get; set; }
        public int PaymentMethod { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
    }
}
