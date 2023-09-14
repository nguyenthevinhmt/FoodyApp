using Foody.Application.Services.CategoryServices.Dtos;
using Foody.Application.Services.CategoryServices.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;

namespace Foody.Application.Services.CategoryServices.Implements
{
    public class CategoryService : ICategoryService
    {
        private readonly FoodyAppContext _context;

        public CategoryService(FoodyAppContext context)
        {
            _context = context;
        }
        public async Task Create(CreateCategoryDto input)
        {
            await _context.Categories.AddAsync(new Category
            {
                Name = input.Name,
                Description = input.Description,
                CreatedAt = DateTime.Now,
                CategoryImageUrl = input.CategoryImageUrl,
            });
            await _context.SaveChangesAsync();
        }
    }
}
