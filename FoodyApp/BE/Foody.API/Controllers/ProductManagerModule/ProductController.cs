using Foody.Application.Services.ProductImageService.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Application.Services.ProductServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.ProductManagerModule
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
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto input)
        {
            try
            {
                await _service.CreateProduct(input);
                return Ok("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
