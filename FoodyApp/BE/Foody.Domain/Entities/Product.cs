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
        public int CategoryId { get; set; }
        public Category Categories { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<ProductPromotion> ProductPromotion { get; set; }
        public bool IsActived { get; set; }
    }
}
