using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Share.Shared.FilterDto;

namespace Foody.Application.Services.PromotionServices.Interfaces
{
    public interface IPromotionService
    {
        public Task CreatePromotion(CreatePromotionDto input);
        public Task<PageResultDto<PromotionResponseDto>> getPromotionPaging(PromotionFilterDto input);
        public Task<PromotionResponseDto> getPromotionById(int id);
        public Task UpdatePromotion(UpdatePromotionDto input);
        public Task DeletePromotion(int id);
    }
}
