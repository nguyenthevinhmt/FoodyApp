using Foody.Application.Services.CategoryServices.Dtos;
using Foody.Application.Services.CategoryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.CategoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        [HttpPost("create-category")]
        public async Task<IActionResult> Create(CreateCategoryDto input)
        {
            await _service.Create(input);
            return Ok();
        }
    }
}
