using System.ComponentModel.DataAnnotations;

namespace Foody.Domain.Entities
{
    public class OrderDetailTemp
    {
        [Key]
        public int Id { get; set; }
        public int referId { get; set; }
        public int Quantity { get; set; }
        public int OrderTempId { get; set; }
        public OrderTemp Order { get; }
        public int ProductId { get; set; }
    }
}
