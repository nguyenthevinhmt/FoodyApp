using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.CartServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.CartController
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service)
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
            try
            {
                return Ok(await _service.GetCartByUserId());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
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
                var result = await _service.AddProductToCart(productId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// xóa sản phẩm khỏi giỏ hàng
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("delete-product-from-cart")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                await _service.RemoveProductFromCart(productId);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Thay đổi số lượng sản phẩm theo id và số lượng
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPut("update-cart-quantity")]
        public async Task<IActionResult> Update(UpdateCartDto input)
        {
            try
            {
                return Ok(await _service.UpdateQuantity(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
