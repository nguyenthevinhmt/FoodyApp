using Foody.Application.Filters;
using Foody.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers
{
    [Authorize]
    [AuthorizationFilter(UserTypes.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PongController : ControllerBase
    {
        [HttpGet]
        public IActionResult Pong()
        {
            return Ok("Pong success");
        }
    }
}
