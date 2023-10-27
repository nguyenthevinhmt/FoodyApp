using Foody.Application.Filters;
using Foody.Application.Services.DashboardServices.Dtos;
using Foody.Application.Services.DashboardServices.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
using Foody.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.DashboardController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService dashboardService)
        {
            _service = dashboardService;
        }

        /// <summary>
        /// Thống kê doanh thu, số đơn bán được theo thời gian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Authorize]
        [AuthorizationFilter(UserTypes.Admin)]
        [HttpGet("get-all-paging")]
        public async Task<IActionResult> getAllPaging([FromQuery] DashboardFilterDto input)
        {
            var result = await _service.GetOrderStatistics(input);
            return Ok(result);
        }


    }
}
