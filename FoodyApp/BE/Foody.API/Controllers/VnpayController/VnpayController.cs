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

        public VnpayController(IVnpayService service)
        {
            _service = service;
        }
        [HttpPost("payment-vn-pay")]
        public IActionResult CreateUrl([FromBody] PaymentInformationDto input)
        {
            return Ok(_service.CreatePaymentUrl(input, HttpContext));
        }
    }
}
