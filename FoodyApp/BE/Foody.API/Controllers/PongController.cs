using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers
{

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
