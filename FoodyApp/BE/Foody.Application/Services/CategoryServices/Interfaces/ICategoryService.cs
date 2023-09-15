using Foody.Application.Services.CategoryServices.Dtos;

namespace Foody.Application.Services.CategoryServices.Interfaces
{
    public interface ICategoryService
    {
        public Task Create(CreateCategoryDto input);
        public Task<CategoryResponseDto> GetCategoryById(int id);
        public Task UpdateCategory(UpdateCategoryDto input);
        public Task DeleteCategory(int id);
    }
}
