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

        public ProductController(IProductService service)
        {
            _service = service;
        }
        /// <summary>
        /// Lấy tất cả sản phẩm, có phân trang và tìm kiếm theo tên sản phẩm, theo khoảng giá
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get-product-paging")]
        public IActionResult getAllPaging([FromQuery] ProductFilterDto input)
        {
            var result = _service.GetProductPaging(input);
            return Ok(result);
        }
        //[HttpPost("create-product")]
        //public 

    }
}
