using Foody.Application.Services.CategoryServices.Dtos;
using Foody.Application.Services.CategoryServices.Interfaces;
using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace Foody.Application.Services.CategoryServices.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly FoodyAppContext _context;
        private readonly IStorageService _storageService;
        private const string FILE_STORE_FOLDER = "ImageStorage";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(FoodyAppContext context, IStorageService storageService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _storageService = storageService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task Create(CreateCategoryDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == input.Name);
            if (category != null)
            {
                throw new UserFriendlyException($"Category's với tên {input.Name} đã tồn tại");
            }
            var categoryCreate = new Category
            {
                Name = input.Name,
                Description = input.Description,
                CreatedAt = DateTime.Now,
                CreatedBy = currentUserId,
            };
            await _context.Categories.AddAsync(categoryCreate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var currentUserId = CommonUtils.GetUserId(this._httpContextAccessor);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (category == null)
            {
                throw new UserFriendlyException($"Category với id = {id} không tồn tại!");
            }
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;
            category.UpdateBy = currentUserId;

            var productsToDelete = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductPromotion)
                .Where(product => product.CategoryId == id);

            foreach (var product in productsToDelete)
            {
                product.IsDeleted = true;

                foreach (var image in product.ProductImages)
                {
                    image.IsDeleted = true;
                }

                foreach (var promotion in product.ProductPromotion)
                {
                    promotion.IsActive = false;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null || category.IsDeleted == true)
            {
                throw new UserFriendlyException($"Category với id = {id} không tồn tại");
            }
            return new CategoryResponseDto
            {
                Id = id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public async Task UpdateCategory(UpdateCategoryDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == input.Id);
            if (category == null || category.IsDeleted == true)
            {
                throw new UserFriendlyException($"Category có id = {input.Id} không tồn tại!");
            }
            category.Name = input.Name;
            category.Description = input.Description;
            category.UpdatedAt = DateTime.Now;
            category.UpdateBy = currentUserId;

            await _context.SaveChangesAsync();

        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + FILE_STORE_FOLDER + "/images/" + fileName;
        }
        public async Task<PageResultDto<CategoryResponseDto>> GetCategoryPaging(CategoryFilterDto input)
        {
            var query = _context.Categories.AsQueryable();
            query = query.Where(c => (c.IsDeleted == false)
            && (input.Name == null || c.Name.ToLower().Trim().Contains(input.Name.ToLower())));

            var totalItem = await query.CountAsync();

            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                .Select(cate => new CategoryResponseDto
                {
                    Id = cate.Id,
                    Name = cate.Name,
                    Description = cate.Description
                }).ToListAsync();

            var pageResult = new PageResultDto<CategoryResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
        }
    }
}
