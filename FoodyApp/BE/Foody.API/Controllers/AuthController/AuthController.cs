using Foody.Application.Services.AuthServices.Dtos;
using Foody.Application.Services.AuthServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.AuthController
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        /// <summary>
        /// Đăng nhập 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto input)
        {
            try
            {
                var tokenApi = _service.Login(input);
                return Ok(tokenApi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Đăng ký tài khoản
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateUserDto input)
        {
            try
            {
                _service.RegisterUser(input);
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// RefreshToken
        /// </summary>
        /// <param name="tokenApiDto"></param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenApiDto tokenApiDto)
        {
            try
            {
                var result = _service.RefreshToken(tokenApiDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
