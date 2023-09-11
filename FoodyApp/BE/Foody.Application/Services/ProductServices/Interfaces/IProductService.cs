using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Shared.FilterDto;

namespace Foody.Application.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        public void CreateProduct(CreateProductDto input);
        public PageResultDto<ProductResponseDto> GetProductPaging(ProductFilterDto input);

        public ProductResponseDto GetProductById(int id);
        public void UpdateProduct(UpdateProductDto input);
        //public PageResultDto<ProductResponseDto> GetProductByCategory(ProductFilterDto input);
    }
}
