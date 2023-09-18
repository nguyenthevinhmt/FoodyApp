using Foody.Application.Exceptions;
using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Services.ProductServices.Interfaces;
using Foody.Application.Shared;
using Foody.Application.Shared.FilterDto;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Foody.Application.Services.ProductServices.Implements
{
    public class ProductService : IProductService
    {
        private readonly FoodyAppContext _context;
        private readonly IStorageService _storageService;
        private const string FILE_STORE_FOLDER = "ImageStorage";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(FoodyAppContext context, IStorageService storageService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _storageService = storageService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreateProduct(CreateProductDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (product != null)
            {
                throw new UserFriendlyException($"Sản phẩm có tên {input.Name} đã tồn tại trong hệ thống");
            }
            var productCreate = new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                ActualPrice = input.ActualPrice,
                CategoryId = input.CategoryId,
                IsActived = input.IsActive,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId.ToString(),
            };
            //Xử lý thêm ảnh ở đây
            if (input.ThumbnailImage != null)
            {
                productCreate.ProductImages = new List<ProductImage>() {
                    new ProductImage()
                    {
                        Description = "",
                        CreatedAt = DateTime.Now,
                        FileSize = input.ThumbnailImage.Length,
                        ProductImageUrl = await this.SaveFile(input.ThumbnailImage)
                    }
                };
            }
            await _context.Products.AddAsync(productCreate);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            //var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == product.PromotionId);
            var image = await _context.ProductImages.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            return new ProductResponseDto
            {
                Id = id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ActualPrice = product.ActualPrice,
                //PromotionId = product.PromotionId,
                ProductImageUrl = image != null ? image.ProductImageUrl : "no-image.jpg",
                CategoryId = product.CategoryId,
                CreateBy = product.CreatedBy
            };
        }

        public async Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input)
        {
            var query = from product in _context.Products
                        join proImage in _context.ProductImages on product.Id equals proImage.ProductId
                        join category in _context.Categories on product.CategoryId equals category.Id
                        //join promotion in _context.Promotions on product.PromotionId equals promotion.Id
                        select new { product, proImage, category };
            //select new { product, proImage, category, promotion };
            query = query.Where(p => (p.product.IsDeleted == false) && (input.Name == null || p.product.Name.ToLower().Trim().Contains(input.Name.ToLower()))
            && ((input.StartPrice <= p.product.ActualPrice && p.product.ActualPrice <= input.EndPrice))
            && (input.CategoryId == null || p.product.CategoryId == Convert.ToInt32(input.CategoryId)));
            var totalItem = await query.CountAsync();
            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                    .Select(p => new ProductResponseDto
                    {
                        Id = p.product.Id,
                        Name = p.product.Name,
                        Price = p.product.Price,
                        ActualPrice = p.product.ActualPrice,
                        Description = p.product.Description,
                        ProductImageUrl = p.proImage.ProductImageUrl,
                        CategoryId = p.category.Id,
                        CategoryName = p.category.Name,
                        //PromotionId = p.promotion.Id,
                        //PromotionName = p.promotion.Name,
                        CreateBy = p.product.CreatedBy
                    }).ToListAsync();
            var pageResult = new PageResultDto<ProductResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
        }

        public async Task UpdateProduct(UpdateProductDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == input.Id);
            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {input.Id} không tồn tại!");
            }
            product.Name = input.Name;
            product.Description = input.Description;
            product.Price = input.Price;
            product.ActualPrice = input.ActualPrice;
            product.UpdatedAt = DateTime.Now;
            product.UpdateBy = currentUserId.ToString();
            product.IsActived = input.IsActive;
            // xử lý cập nhật ảnh
            if (input.ThumbnailImage != null)
            {
                var thumbnailImage = _context.ProductImages.FirstOrDefault(i => i.ProductId == input.Id);
                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = input.ThumbnailImage.Length;
                    thumbnailImage.ProductImageUrl = await this.SaveFile(input.ThumbnailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.IsDeleted == false && p.Id == id);
            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            product.IsDeleted = true;
            product.UpdatedAt = DateTime.Now;
            product.UpdateBy = currentUserId.ToString();
            await _context.SaveChangesAsync();
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + FILE_STORE_FOLDER + "/images/" + fileName;
        }


    }
}
