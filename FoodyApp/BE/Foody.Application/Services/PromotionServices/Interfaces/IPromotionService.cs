using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Share.Shared.FilterDto;

namespace Foody.Application.Services.PromotionServices.Interfaces
{
    public interface IPromotionService
    {
        public Task CreatePromotion(CreatePromotionDto input);
        public Task<PageResultDto<PromotionResponseDto>> GetPromotionPaging(PromotionFilterDto input);
        public Task<PromotionResponseDto> GetById(int id);
        public Task Delete(int id);
        public Task Update(UpdatePromotionDto input);
    }
}
