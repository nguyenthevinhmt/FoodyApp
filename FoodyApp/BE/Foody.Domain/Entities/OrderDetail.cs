namespace Foody.Domain.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; }
        public int ProductId { get; set; }
        public Product Product { get; }
    }
}
