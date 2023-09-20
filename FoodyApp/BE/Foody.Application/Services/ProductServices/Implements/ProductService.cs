using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Services.ProductServices.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Foody.Share.Shared.FilterDto;
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
        public async Task<int> CreateProduct(CreateProductDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == input.Name);
            if (product != null)
            {
                throw new UserFriendlyException($"Sản phẩm có tên {input.Name} đã tồn tại trong hệ thống");
            }
            if (!await _context.Promotions.AnyAsync(c => c.Id == input.PromotionId))
            {
                throw new UserFriendlyException("Chương trình khuyến mãi không tồn tại");
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
            var productPromotion = new ProductPromotion
            {
                ProductId = productCreate.Id,
                PromotionId = input.PromotionId
            };
            await _context.ProductPromotions.AddAsync(productPromotion);
            await _context.SaveChangesAsync();
            return productCreate.Id;
        }

        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var product = await (from prod in _context.Products
                                 join pp in _context.ProductPromotions on prod.Id equals pp.ProductId
                                 join category in _context.Categories on prod.CategoryId equals category.Id
                                 join prom in _context.Promotions on pp.PromotionId equals prom.Id
                                 where prod.Id == id && prod.IsDeleted == false
                                       && prod.IsActived == true && prom.IsActive == true && prom.IsDeleted == false
                                 select new ProductResponseDto
                                 {
                                     Id = prod.Id,
                                     Name = prod.Name,
                                     Price = prod.Price,
                                     Description = prod.Description,
                                     ActualPrice = prod.ActualPrice,
                                     CategoryId = prod.CategoryId,
                                     CreateBy = prod.CreatedBy,
                                     PromotionId = prom.Id,
                                     PromotionName = prom.Name
                                 }).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            var image = await _context.ProductImages.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            if (image != null)
            {
                product.ProductImageUrl = image.ProductImageUrl;
            }
            else
            {
                product.ProductImageUrl = "no-image.png";
            }

            return product;
        }

        public async Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input)
        {
            var query = from product in _context.Products
                        join category in _context.Categories on product.CategoryId equals category.Id
                        join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                        join promotion in _context.Promotions on productPromotion.PromotionId equals promotion.Id
                        join proImage in _context.ProductImages on product.Id equals proImage.ProductId
                        select new { product, proImage, category, promotion };
            query = query.Where(p => (p.product.IsDeleted == false)
            && (p.product.IsActived == true) && (p.promotion.IsActive == true)
            && (input.Name == null || p.product.Name.ToLower().Trim().Contains(input.Name.ToLower()))
            && ((input.StartPrice <= p.product.ActualPrice && p.product.ActualPrice <= input.EndPrice))
            && (input.CategoryId == null || p.category.Id == Convert.ToInt32(input.CategoryId)));
            var totalItem = await query.CountAsync();
            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                    .Select(p => new ProductResponseDto
                    {
                        Id = p.product.Id,
                        Name = p.product.Name,
                        Description = p.product.Description,
                        ActualPrice = p.product.ActualPrice,
                        Price = p.product.Price,
                        CategoryId = p.category.Id,
                        CategoryName = p.category.Name,
                        ProductImageUrl = p.proImage.ProductImageUrl,
                        PromotionId = p.promotion.Id,
                        PromotionName = p.promotion.Name,
                        CreateBy = p.product.CreatedBy,
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

        public async Task UpdatePromotionToProduct(int promotionId, int productId)
        {
            if (!await _context.Promotions.AnyAsync(c => c.Id == promotionId))
            {
                throw new UserFriendlyException($"Khuyến mại có ID = {promotionId} không tồn tại!");
            }
            else if (!await _context.Products.AnyAsync(c => c.Id == productId))
            {
                throw new UserFriendlyException($"Sản phẩm có ID = {productId} không tồn tại!");
            }
            else
            {
                await _context.ProductPromotions.AddAsync(new ProductPromotion
                {
                    PromotionId = promotionId,
                    ProductId = productId,
                    IsActive = true
                });
                await _context.SaveChangesAsync();
            }
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
