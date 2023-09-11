using Foody.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class Product : BaseEntity<int>
    {
        [StringLength(250)]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Name { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        [Range(0, double.MaxValue)]
        public decimal ActualPrice { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [StringLength(500)]
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public IEnumerable<Category> Categories { get; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public IEnumerable<ProductCart> ProductCarts { get; }
        public IEnumerable<OrderDetail> OrderDetails { get; }
        public IEnumerable<ProductImage> ProductImages { get; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Cart> Carts { get; }
        public bool IsActived { get; set; }
    }
}
