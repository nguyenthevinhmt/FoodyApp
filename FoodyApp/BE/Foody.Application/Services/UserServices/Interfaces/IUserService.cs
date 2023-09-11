using Foody.Application.Services.UserServices.Dtos;

namespace Foody.Application.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        public void UpdateUserInfo(UpdateUserDto input);
        public UserResponseDto GetById(int id);
    }
}
