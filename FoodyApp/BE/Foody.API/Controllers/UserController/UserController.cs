using Foody.Application.Services.UserServices.Dtos;
using Foody.Application.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [HttpGet("get-by-id/{id}")]
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
        /// <summary>
        /// Lấy tất cả địa chỉ người dùng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("get-user-address-paging")]
        public async Task<IActionResult> GetAllUserAddress([FromQuery] AddressFilterDto input)
        {
            try
            {
                var result = await _service.GetAddressForUserPaging(input);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Thêm địa chỉ cho người dùng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("create-new-address")]
        public async Task<IActionResult> CreateNewAddress(CreateAddressDto input)
        {
            await _service.AddAddressForUser(input);
            return Ok("Thêm địa chỉ thành công");
        }
        /// <summary>
        /// Cập nhật địa chỉ 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("update-address")]
        public async Task<IActionResult> Update(UpdateAddressDto input)
        {
            try
            {
                await _service.UpdateAddressForUser(input);
                return Ok("Cập nhật địa chỉ thành công");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
