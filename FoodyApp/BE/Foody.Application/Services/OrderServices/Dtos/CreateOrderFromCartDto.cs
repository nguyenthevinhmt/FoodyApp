namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateOrderFromCartDto
    {
        public int OrderTempId { get; set; }
        public bool IsPaid { get; set; }
    }
}
