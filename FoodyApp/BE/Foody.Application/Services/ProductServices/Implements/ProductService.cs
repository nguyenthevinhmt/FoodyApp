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
        //Tạo mới sản phẩm
        public async Task<string> CreateProduct(CreateProductDto input)
        {
            using (var CreateTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
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
                        CreatedBy = currentUserId,
                    };

                    //Xử lý thêm ảnh ở đây
                    if (input.ThumbnailImage != null)
                    {
                        productCreate.ProductImages = new List<ProductImage>() {
                            new ProductImage()
                            {
                                Description = $"Ảnh mô tả sản phẩm {input.Name}",
                                CreatedAt = DateTime.Now,
                                CreatedBy = currentUserId,
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
                    await CreateTransaction.CommitAsync();
                    return "Thêm sản phẩm thành công";
                }
                catch (Exception ex)
                {
                    await CreateTransaction.RollbackAsync();
                    return $"Lỗi: {ex.Message}";
                }
            }

        }
        //Lấy sản phẩm theo id
        public async Task<ProductResponseDto> GetProductById(int id)
        {
            var query = await (from product in _context.Products
                               where product.Id == id
                               join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                               into pi
                               from proImg in pi.DefaultIfEmpty()
                               join category in _context.Categories on product.CategoryId equals category.Id
                               join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                               join promotion in _context.Promotions on productPromotion.PromotionId equals promotion.Id
                               select new ProductResponseDto
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   Description = product.Description,
                                   ActualPrice = product.ActualPrice - (product.ActualPrice * promotion.DiscountPercent / 100),
                                   Price = product.Price,
                                   CategoryId = product.CategoryId,
                                   CategoryName = category.Name,
                                   ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                                   Promotion = new PromotionResponseDto
                                   {
                                       Name = promotion.Name,
                                       Description = promotion.Description,
                                       PromotionCode = promotion.PromotionCode,
                                       DiscountPercent = promotion.DiscountPercent,
                                       IsActive = promotion.IsActive,
                                   },
                                   CreateBy = product.CreatedBy,
                                   IsDeleted = product.IsDeleted,
                                   IsActive = product.IsActived
                               }).FirstOrDefaultAsync();

            if (query == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            return query;
        }

        //Lấy tất cả sản phẩm phân trang
        public async Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input)
        {
            var query = from product in _context.Products
                        join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                        into pi
                        from proImg in pi.DefaultIfEmpty()
                        join category in _context.Categories on product.CategoryId equals category.Id
                        join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                        join promotion in _context.Promotions on productPromotion.PromotionId equals promotion.Id
                        select new ProductResponseDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            ActualPrice = product.ActualPrice - (product.ActualPrice * promotion.DiscountPercent / 100),
                            Price = product.Price,
                            CategoryId = product.CategoryId,
                            CategoryName = category.Name,
                            ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                            Promotion = new PromotionResponseDto
                            {
                                Name = promotion.Name,
                                Description = promotion.Description,
                                PromotionCode = promotion.PromotionCode,
                                DiscountPercent = promotion.DiscountPercent,
                                IsActive = promotion.IsActive,
                            },
                            CreateBy = product.CreatedBy,
                            IsDeleted = product.IsDeleted,
                            IsActive = product.IsActived
                        };
            query = query.Where(p => (p.IsDeleted == false)
            && (p.IsActive == true) && (p.Promotion.IsActive == true)
            && (input.Name == null || p.Name.ToLower().Trim().Contains(input.Name.ToLower()))
            && ((input.StartPrice <= p.ActualPrice && p.ActualPrice <= input.EndPrice))
            && (input.CategoryId == null || p.CategoryId == Convert.ToInt32(input.CategoryId)));
            var totalItem = await query.CountAsync();
            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                    .Select(prod => new ProductResponseDto
                    {
                        Id = prod.Id,
                        Name = prod.Name,
                        Description = prod.Description,
                        ActualPrice = prod.ActualPrice,
                        Price = prod.Price,
                        CategoryId = prod.Id,
                        ProductImageUrl = prod.ProductImageUrl,
                        Promotion = prod.Promotion,
                        CreateBy = prod.CreateBy,
                        IsDeleted = prod.IsDeleted,
                        IsActive = prod.IsActive
                    }).ToListAsync();
            var pageResult = new PageResultDto<ProductResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
        }

        public async Task<PageResultDto<ProductResponseDto>> GetProductsByCategoryIdPaging(ProductFilterByCategoryDto input)
        {
            var query = from product in _context.Products
                        join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                        into pi
                        from proImg in pi.DefaultIfEmpty()
                        join category in _context.Categories on product.CategoryId equals category.Id
                        join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                        join promotion in _context.Promotions on productPromotion.PromotionId equals promotion.Id
                        select new ProductResponseDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            ActualPrice = product.ActualPrice - (product.ActualPrice * promotion.DiscountPercent / 100),
                            Price = product.Price,
                            CategoryId = product.CategoryId,
                            CategoryName = category.Name,
                            ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                            Promotion = new PromotionResponseDto
                            {
                                Name = promotion.Name,
                                Description = promotion.Description,
                                PromotionCode = promotion.PromotionCode,
                                DiscountPercent = promotion.DiscountPercent,
                                IsActive = promotion.IsActive,
                            },
                            CreateBy = product.CreatedBy,
                            IsDeleted = product.IsDeleted,
                            IsActive = product.IsActived,
                        };
            query = query.Where(p => (p.IsDeleted == false)
            && (p.IsActive == true) && (p.Promotion.IsActive == true)
            && (p.CategoryId == Convert.ToInt32(input.CategoryId)));
            var totalItem = await query.CountAsync();
            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToListAsync();
            var pageResult = new PageResultDto<ProductResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
        }

        //Cập nhật thông tin sản phẩm
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
            product.UpdateBy = currentUserId;
            product.IsActived = input.IsActive;
            // xử lý cập nhật ảnh
            if (input.ThumbnailImage != null)
            {
                var thumbnailImage = _context.ProductImages.FirstOrDefault(i => i.ProductId == input.Id);
                if (thumbnailImage != null)
                {
                    await _storageService.DeleteFileAsync(thumbnailImage.ProductImageUrl);
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
            var images = _context.ProductImages.Where(i => i.ProductId == id);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ProductImageUrl);
            }
            product.IsDeleted = true;
            product.UpdatedAt = DateTime.Now;
            product.UpdateBy = currentUserId;
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
                var productPromotion = await _context.ProductPromotions.FirstOrDefaultAsync(c => c.ProductId == productId);
                productPromotion.PromotionId = promotionId;
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

        //Lấy tất cả sản phẩm được giảm giá
        public async Task<PageResultDto<ProductResponseDto>> GetProductDiscountedPaging(FilterDto input)
        {
            var query = from product in _context.Products
                        join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                        into pi
                        from proImg in pi.DefaultIfEmpty()
                        join category in _context.Categories on product.CategoryId equals category.Id
                        join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                        join promotion in _context.Promotions on productPromotion.PromotionId equals promotion.Id
                        select new ProductResponseDto
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            ActualPrice = product.ActualPrice - (product.ActualPrice * promotion.DiscountPercent / 100),
                            Price = product.Price,
                            CategoryId = product.CategoryId,
                            CategoryName = category.Name,
                            ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                            Promotion = new PromotionResponseDto
                            {
                                Name = promotion.Name,
                                Description = promotion.Description,
                                PromotionCode = promotion.PromotionCode,
                                DiscountPercent = promotion.DiscountPercent,
                                IsActive = promotion.IsActive,
                            },
                            CreateBy = product.CreatedBy,
                            IsDeleted = product.IsDeleted,
                            IsActive = product.IsActived
                        };

            query = query.Where(p => (p.IsDeleted == false)
            && (p.IsActive == true) && (p.Promotion.IsActive == true)
            && (p.Promotion.DiscountPercent != 0));

            var totalItem = await query.CountAsync();
            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                    .Select(prod => new ProductResponseDto
                    {
                        Id = prod.Id,
                        Name = prod.Name,
                        Description = prod.Description,
                        ActualPrice = prod.ActualPrice,
                        Price = prod.Price,
                        CategoryId = prod.Id,
                        ProductImageUrl = prod.ProductImageUrl,
                        Promotion = prod.Promotion,
                        CreateBy = prod.CreateBy,
                        IsDeleted = prod.IsDeleted,
                        IsActive = prod.IsActive
                    }).ToListAsync();
            var pageResult = new PageResultDto<ProductResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
        }
    }
}
