using Foody.Application.Services.ProductServices.Dtos;
using Foody.Share.Shared.FilterDto;

namespace Foody.Application.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        public Task<string> CreateProduct(CreateProductDto input);
        public Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input);
        public Task<PageResultDto<ProductResponseDto>> GetProductsByCategoryIdPaging(ProductFilterByCategoryDto input);

        public Task<ProductResponseDto> GetProductById(int id);
        public Task UpdateProduct(UpdateProductDto input);
        public Task DeleteProduct(int id);
        public Task UpdatePromotionToProduct(int promotionId, int productId);
        public Task<PageResultDto<ProductResponseDto>> GetProductDiscountedPaging(FilterDto input);
        //public Task<PageResultDto<ProductResponseDto>> GetProductPagingAdmin(ProductFilterDto input);
    }
}
