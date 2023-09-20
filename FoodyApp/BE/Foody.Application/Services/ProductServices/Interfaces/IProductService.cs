using Foody.Application.Services.ProductServices.Dtos;
using Foody.Share.Shared.FilterDto;

namespace Foody.Application.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        public Task<int> CreateProduct(CreateProductDto input);
        public Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input);

        public Task<ProductResponseDto> GetProductById(int id);
        public Task UpdateProduct(UpdateProductDto input);
        public Task DeleteProduct(int id);
        public Task UpdatePromotionToProduct(int promotionId, int productId);
    }
}
