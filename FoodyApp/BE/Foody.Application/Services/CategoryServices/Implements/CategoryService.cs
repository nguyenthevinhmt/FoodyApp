using Foody.Application.Exceptions;
using Foody.Application.Services.CategoryServices.Dtos;
using Foody.Application.Services.CategoryServices.Interfaces;
using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
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

        public CategoryService(FoodyAppContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task Create(CreateCategoryDto input)
        {
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
                //Thêm ảnh
                CategoryImageUrl = await this.SaveFile(input.ThumbnailImage)
            };
            await _context.Categories.AddAsync(categoryCreate);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);
            if (category == null)
            {
                throw new UserFriendlyException($"Category với id = {id} không tồn tại!");
            }
            category.IsDeleted = true;
            category.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }

        public async Task<CategoryResponseDto> GetCategoryById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                throw new UserFriendlyException($"Category với id = {id} không tồn tại");
            }
            return new CategoryResponseDto
            {
                Id = id,
                Name = category.Name,
                Description = category.Description,
                CategoryImageUrl = category.CategoryImageUrl != null ? category.CategoryImageUrl : "no-image.jpg",
            };
        }

        public async Task UpdateCategory(UpdateCategoryDto input)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == input.Id);
            if (category == null)
            {
                throw new UserFriendlyException($"Category có id = {input.Id} không tồn tại!");
            }
            category.Name = input.Name;
            category.Description = input.Description;
            category.CategoryImageUrl = await this.SaveFile(input.ThumbnailImage);
            _context.SaveChanges();
            
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
