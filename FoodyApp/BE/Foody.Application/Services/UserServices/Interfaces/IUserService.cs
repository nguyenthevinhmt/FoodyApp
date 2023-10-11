using Foody.Application.Services.UserServices.Dtos;
using Foody.Share.Shared.FilterDto;

namespace Foody.Application.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        public void UpdateUserInfo(UpdateUserDto input);
        public UserResponseDto GetById(int id);
        public Task AddAddressForUser(CreateAddressDto input);
        public Task<PageResultDto<AddressResponseDto>> GetAddressForUserPaging(AddressFilterDto input);
        public Task UpdateAddressForUser(UpdateAddressDto input);
    }
}
