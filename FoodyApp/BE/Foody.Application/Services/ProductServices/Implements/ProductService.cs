using Foody.Application.Exceptions;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Services.ProductServices.Interfaces;
using Foody.Application.Shared.FilterDto;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;

namespace Foody.Application.Services.ProductServices.Implements
{
    public class ProductService : IProductService
    {
        private readonly FoodyAppContext _context;

        public ProductService(FoodyAppContext context)
        {
            _context = context;
        }
        public void CreateProduct(CreateProductDto input)
        {
            var product = _context.Products.FirstOrDefault(p => p.Name == input.Name);
            if (product != null)
            {
                throw new UserFriendlyException($"Sản phẩm có tên {input.Name} đã tồn tại trong hệ thống");
            }
            _context.Products.Add(new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                ActualPrice = input.ActualPrice,
                ProductImageUrl = input.ProductImageUrl
            });
            _context.SaveChanges();
        }

        public ProductResponseDto GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            return new ProductResponseDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ActualPrice = product.ActualPrice,
                ProductImageUrl = product.ProductImageUrl
            };
        }

        public PageResultDto<ProductResponseDto> GetProductPaging(ProductFilterDto input)
        {
            var query = _context.Products.AsQueryable();
            query = query.Where(p => (input.Name == null || p.Name.ToLower().Trim().Contains(input.Name.ToLower()))
            && (input.StartPrice <= p.ActualPrice && p.ActualPrice <= input.EndPrice));
            var totalItem = query.Count();
            var listItem = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize);
            var result = listItem.Select(p => new ProductResponseDto
            {
                Name = p.Name,
                Price = p.Price,
                ActualPrice = p.ActualPrice,
                ProductImageUrl = p.ProductImageUrl,
                Description = p.Description
            });
            return new PageResultDto<ProductResponseDto>
            {
                Item = result,
                TotalItem = totalItem
            };
        }

        public void UpdateProduct(UpdateProductDto input)
        {
            var product = _context.Products.FirstOrDefault(p => p.IsDeleted == false && p.Id == input.Id);
            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {input.Id} không tồn tại!");
            }
            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.ActualPrice = input.ActualPrice;
            product.ProductImageUrl = input.ProductImageUrl;
            product.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
