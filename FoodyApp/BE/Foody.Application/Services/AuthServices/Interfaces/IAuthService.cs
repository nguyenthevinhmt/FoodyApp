using Foody.Application.Services.AuthServices.Dtos;

namespace Foody.Application.Services.AuthServices.Interfaces
{
    public interface IAuthService
    {
        public TokenApiDto Login(UserLoginDto user);
        public TokenApiDto RefreshToken(TokenApiDto input);
        public void RegisterUser(CreateUserDto user);
    }
}
