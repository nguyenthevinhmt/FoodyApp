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
        /// <summary>
        /// Tạo mới category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create-category")]
        public async Task<IActionResult> Create([FromQuery] CreateCategoryDto input)
        {
            await _service.Create(input);
            return Ok();
        }
        /// <summary>
        /// lấy category theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-catgory-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetCategoryById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update-catgory")]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDto input)
        {
            try
            {
                await _service.UpdateCategory(input);
                return Ok("update thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Xóa category
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-catgory")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteCategory(id);
                return Ok("xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
