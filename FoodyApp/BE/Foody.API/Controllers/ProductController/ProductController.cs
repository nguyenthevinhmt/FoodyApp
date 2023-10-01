﻿using Foody.Application.Filters;
using Foody.Application.Services.FileStoreService.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Services.ProductServices.Interfaces;
using Foody.Share.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.ProductController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IStorageService _storageService;
        public ProductController(IProductService service, IStorageService storageService)
        {
            _service = service;
            _storageService = storageService;
        }
        /// <summary>
        /// Lấy tất cả sản phẩm, có phân trang và tìm kiếm theo tên sản phẩm, theo khoảng giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get-product-paging")]
        public async Task<IActionResult> getAllPaging([FromQuery] ProductFilterDto input)
        {
            var result = await _service.GetProductPaging(input);
            return Ok(result);
        }
        /// <summary>
        /// Tạo mới sản phẩm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto input)
        {
            try
            {
                var result = await _service.CreateProduct(input);
                return Ok($"Thêm thành công, sản phẩm có ID = {result}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Lấy sản phẩm theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _service.GetProductById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Cập nhật thông tin sản phẩm
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update-product")]
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        public async Task<IActionResult> Update([FromForm] UpdateProductDto input)
        {
            try
            {
                await _service.UpdateProduct(input);
                return Ok("Update thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Xóa thông tin sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete-product/{id}")]
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteProduct(id);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Thêm chương trình khuyến mại sản phẩm
        /// </summary>
        /// <param name="promotionId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost("update-promotion")]
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        public async Task<IActionResult> UpdatePromotionToProduct(int promotionId, int productId)
        {
            try
            {
                await _service.UpdatePromotionToProduct(promotionId, productId);
                return Ok("Thêm khuyến mãi thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}