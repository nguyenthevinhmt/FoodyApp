using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.PromotionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _service;

        public PromotionController(IPromotionService service)
        {
            _service = service;
        }
        /// <summary>
        /// Tạo mới chương trình khuyến mãi
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create-promotion")]
        public async Task<IActionResult> Create(CreatePromotionDto input)
        {
            await _service.CreatePromotion(input);
            return Ok();
        }
        /// <summary>
        /// Lấy danh sách khuyến mại có phân trang, tìm kiếm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get-all-promotion")]
        public async Task<IActionResult> GetAllPaging([FromQuery] PromotionFilterDto input)
        {
            var result = await _service.GetPromotionPaging(input);
            return Ok(result);
        }
        /// <summary>
        /// Lấy chương trình khuyến mại theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-promotion-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-promotion")]
        public async Task<IActionResult> Update(UpdatePromotionDto input)
        {
            try
            {
                await _service.Update(input);
                return Ok("Cập nhật thông tin chương trình khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-promotion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.Delete(id);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
