using Foody.Application.Filters;
using Foody.Application.Services.CategoryServices.Dtos;
using Foody.Application.Services.CategoryServices.Interfaces;
using Foody.Share.Constants;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPost("create-category")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto input)
        {
            await _service.Create(input);
            return Ok();
        }
        /// <summary>
        /// Lấy tất cả category, có phân trang, tìm kiếm theo tên
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get-category-paging")]
        public async Task<IActionResult> getAllPaging([FromQuery] CategoryFilterDto input)
        {
            var result = await _service.GetCategoryPaging(input);
            return Ok(result);
        }
        /// <summary>
        /// lấy category theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-catgory-by-id/{id}")]
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
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPut("update-catgory")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDto input)
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
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpDelete("delete-catgory/{id}")]
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
