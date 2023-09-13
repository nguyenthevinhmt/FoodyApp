using Foody.Application.Services.CategoryServices.Dtos;

namespace Foody.Application.Services.CategoryServices.Interfaces
{
    public interface ICategoryService
    {
        public Task Create(CreateCategoryDto input);
    }
}
