﻿using Foody.Application.Services.FileStoreService.Interfaces;
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
            var query = await (from product in _context.Products
                               join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                               into pi
                               from proImg in pi.DefaultIfEmpty()
                               join category in _context.Categories on product.CategoryId equals category.Id
                               into ppic
                               from cate in ppic.DefaultIfEmpty()
                               join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                               into picppm
                               from proImgCatePP in picppm.DefaultIfEmpty()
                               join promotion in _context.Promotions on proImgCatePP.PromotionId equals promotion.Id into results
                               from result in results.DefaultIfEmpty()
                               select new ProductResponseDto
                               {
                                   Id = product.Id,
                                   Name = product.Name,
                                   Description = product.Description,
                                   ActualPrice = product.ActualPrice,
                                   Price = product.Price,
                                   CategoryId = product.CategoryId,
                                   CategoryName = cate.Name,
                                   ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                                   Promotion = result,
                                   CreateBy = product.CreatedBy,
                                   IsDeleted = product.IsDeleted,
                                   IsActive = product.IsActived
                               }).Where(c => c.Id == id).FirstOrDefaultAsync();

            if (query == null)
            {
                throw new UserFriendlyException($"Sản phẩm có id = {id} không tồn tại!");
            }
            return query;
        }

        public async Task<PageResultDto<ProductResponseDto>> GetProductPaging(ProductFilterDto input)
        {
            var query = from product in _context.Products
                        join productImage in _context.ProductImages on product.Id equals productImage.ProductId
                        into pi
                        from proImg in pi.DefaultIfEmpty()
                        join category in _context.Categories on product.CategoryId equals category.Id
                        into ppic
                        from cate in ppic.DefaultIfEmpty()
                        join productPromotion in _context.ProductPromotions on product.Id equals productPromotion.ProductId
                        into picppm
                        from proImgCatePP in picppm.DefaultIfEmpty()
                        join promotion in _context.Promotions on proImgCatePP.PromotionId equals promotion.Id into results
                        from result in results.DefaultIfEmpty()
                        select new
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Description = product.Description,
                            ActualPrice = product.ActualPrice,
                            Price = product.Price,
                            CategoryId = product.CategoryId,
                            CategoryName = cate.Name,
                            ProductImageUrl = proImg.ProductImageUrl != null ? proImg.ProductImageUrl : null,
                            Promotion = result,
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
                        CategoryName = prod.Name,
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


    }
}
