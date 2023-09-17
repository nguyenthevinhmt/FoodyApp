namespace Foody.Application.Services.PromotionServices.Dtos
{
    public class UpdatePromotionDto : CreatePromotionDto
    {
        public int Id { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
