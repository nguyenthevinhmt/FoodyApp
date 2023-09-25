namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateDraftOrderDto
    {
        public int UserId { get; set; }
        public int Status { get; set; }
        public int ProductCartId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
