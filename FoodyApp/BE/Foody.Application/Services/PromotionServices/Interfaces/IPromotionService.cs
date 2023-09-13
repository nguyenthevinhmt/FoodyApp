using Foody.Application.Services.PromotionServices.Dtos;

namespace Foody.Application.Services.PromotionServices.Interfaces
{
    public interface IPromotionService
    {
        public Task CreatePromotion(CreatePromotionDto input);
    }
}
