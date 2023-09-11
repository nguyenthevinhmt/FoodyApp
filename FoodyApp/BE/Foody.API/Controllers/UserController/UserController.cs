using Foody.Application.Services.UserServices.Dtos;
using Foody.Application.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Foody.API.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        /// <summary>
        /// Cập nhật thông tin người dùng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update-user-infor")]
        public IActionResult UpdateUserInfo([FromBody] UpdateUserDto input)
        {
            try
            {
                _service.UpdateUserInfo(input);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Lấy thông tin người dùng theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get-by-id")]
        public IActionResult GetUserInfoById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
