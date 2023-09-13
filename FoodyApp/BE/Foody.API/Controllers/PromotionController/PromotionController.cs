using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
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
        public async Task<IActionResult> Create(CreatePromotionDto input)
        {
            await _service.CreatePromotion(input);
            return Ok();
        }
    }
}
