namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateCartDto
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
