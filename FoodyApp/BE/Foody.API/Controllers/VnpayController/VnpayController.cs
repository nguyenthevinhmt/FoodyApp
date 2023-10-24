using Foody.Application.Services.VnpayService.Dtos;
using Foody.Application.Services.VnpayService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.VnpayController
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        private readonly IVnpayService _service;
        //private readonly HttpContext _context;

        public VnpayController(IVnpayService service)
        {
            _service = service;
            //_context = context;
        }
        [HttpPost("payment-vn-pay")]
        public IActionResult CreateUrl([FromBody] PaymentInformationDto input)
        {
            return Ok(_service.CreatePaymentUrl(input, HttpContext));
        }
        [Route("PaymentCallback")]
        public IActionResult PaymentCallback()
        {
            var response = _service.PaymentExecute(Request.Query);

            return Ok(response);
        }
    }
}
