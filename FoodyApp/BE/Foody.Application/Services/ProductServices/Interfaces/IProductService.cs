﻿using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Shared.FilterDto;

namespace Foody.Application.Services.ProductServices.Interfaces
{
    public interface IProductService
    {
        public Task CreateProduct(CreateProductDto input);
        public Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input);

        public Task<ProductResponseDto> GetProductById(int id);
        public Task UpdateProduct(UpdateProductDto input);
        //public PageResultDto<ProductResponseDto> GetProductByCategory(ProductFilterDto input);
    }
}
