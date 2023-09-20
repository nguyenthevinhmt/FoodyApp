﻿using Foody.Application.Filters;
using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
using Foody.Share.Constants;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
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
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPaging([FromQuery] PromotionFilterDto input)
        {
            var result = await _service.getPromotionPaging(input);
            return Ok(result);
        }
        /// <summary>
        /// Lấy phiếu giảm giá theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-promotion-by-id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.getPromotionById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật khuyến mại
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPut("update-promotion")]
        public async Task<IActionResult> Update([FromForm] UpdatePromotionDto input)
        {
            try
            {
                await _service.UpdatePromotion(input);
                return Ok("Update thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Xóa khuyến mại
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpDelete("delete-promotion/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeletePromotion(id);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
