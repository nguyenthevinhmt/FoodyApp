using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class ProductPromotion
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int PromotionId { get; set; }
        public Promotion Promotion { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
