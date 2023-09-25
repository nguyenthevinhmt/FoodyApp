using Foody.Application.Services.OrderServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.CartController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IOrderService _service;
        public CartController(IOrderService service)
        {
            _service = service;
        }
        /// <summary>
        /// Lấy tất cả sản phẩm tồn tại trong giỏ hàng, tổng giá 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-cart-by-user")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetDraftOrdersByUserId());
        }
        /// <summary>
        /// Thêm sản phẩm vào giỏ hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost("add-product-to-cart")]
        public async Task<IActionResult> Create(int productId)
        {
            try
            {
                var result = await _service.AddProductToDraftOrder(productId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-product-from-cart")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                await _service.RemoveProductFromDraftOrder(productId);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
