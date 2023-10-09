using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.OrderController
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //private readonly IOrderService _service;

        //public OrderController(IOrderService service)
        //{
        //    _service = service;
        //}
        ///// <summary>
        ///// Lấy tất cả đơn hàng đang chờ xử lý
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all-pending-order")]
        //public async Task<IActionResult> GetPendingOrder()
        //{
        //    try
        //    {
        //        return Ok(await _service.GetPendingOrder());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Lấy tất cả đơn hàng đang ship
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all-shipping-order")]
        //public async Task<IActionResult> GetShippingOrder()
        //{
        //    try
        //    {
        //        return Ok(await _service.GetShippingOrder());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Lấy tất cả đơn hàng đã mua 
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all-succes-order")]
        //public async Task<IActionResult> GetSuccessOrder()
        //{
        //    try
        //    {
        //        return Ok(await _service.GetSuccessOrder());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        ///// <summary>
        ///// Lấy tất cả đơn hàng đã hủy
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("get-all-cancel-order")]
        //public async Task<IActionResult> GetCancelOrder()
        //{
        //    try
        //    {
        //        return Ok(await _service.GetCanceledOrder());
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpPut("update-order-status")]
        //public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusDto input)
        //{
        //    try
        //    {
        //        await _service.UpdateOrderStatus(input);
        //        return Ok("Success");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
