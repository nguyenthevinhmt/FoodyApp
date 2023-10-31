using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }
        /// <summary>
        /// Lấy đơn hàng theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetOrderById(id));
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đang chờ xử lý
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-pending-order")]
        public async Task<IActionResult> GetPendingOrder()
        {
            try
            {
                return Ok(await _service.GetPendingOrder());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đã được admin accept
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-accepeted-order")]
        public async Task<IActionResult> GetAllAcceptedOrder()
        {
            try
            {
                return Ok(await _service.GetAcceptOrder());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đang ship
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-shipping-order")]
        public async Task<IActionResult> GetShippingOrder()
        {
            try
            {
                return Ok(await _service.GetShippingOrder());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đã mua 
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-success-order")]
        public async Task<IActionResult> GetSuccessOrder()
        {
            try
            {
                return Ok(await _service.GetSuccessOrder());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đã hủy
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-all-cancel-order")]
        public async Task<IActionResult> GetCancelOrder()
        {
            try
            {
                return Ok(await _service.GetCanceledOrder());
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPut("update-order-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusDto input)
        {
            try
            {
                await _service.UpdateOrderStatus(input);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// tạo đơn hàng trực tiếp từ sản phẩm, trả về id đơn hàng vừa dc tạo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create-order")]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto input)
        {
            try
            {
                return Ok(await _service.CreateOrder(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// tạo đơn hàng từ giỏ hàng, trả về id đơn hàng vừa tạo
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create-order-from-cart")]
        public async Task<IActionResult> CreateFromCart([FromBody] CreateOrderFromCartDto input)
        {
            try 
            {
                return Ok(await _service.CreateOrderFromCart(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Lấy tất cả đơn hàng role admin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("admin/get-all-order")]
        public async Task<IActionResult> GetListOrderAdmin([FromQuery] OrderFilterDto input)
        {
            try
            {
                return Ok(await _service.GetAllOrders(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// xóa đơn hàng được tạo trực tiếp từ sản phẩm đang ở trạng thái chờ
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("delete-order")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                await _service.DeleteOrder(orderId);
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// xử lý khi đơn hàng thanh toán thành công
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpPut("order-paid-success")]
        public async Task<IActionResult> OrderPaidSuccess(int orderId)
        {
            try
            {
                await _service.OrderPaidSuccessResponse(orderId); 
                return Ok("Thanh toán thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// xử lý khi đơn hàng từ giỏ hàng thanh toán thất bại
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("order-from-cart-fail")]
        public async Task<IActionResult> OrderFromCartFail(OrderCartFailFilterDto input)
        {
            try
            {
                await _service.OrderFromCartFailResponse(input);
                return Ok("rollback giỏ hàng thành công");
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
