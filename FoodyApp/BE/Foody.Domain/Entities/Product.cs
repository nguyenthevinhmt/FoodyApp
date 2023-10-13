using Foody.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class Product : BaseEntity<int>, ICreated, IUpdated, ISoftDeleted
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, double.MaxValue)]
        public double ActualPrice { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(500)]
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ProductPromotion> ProductPromotion { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<ProductCart> ProductCarts { get; set; }

        public bool IsActived { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdateBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
    }
}
