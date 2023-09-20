using Foody.Application.Services.UserServices.Dtos;
using Foody.Application.Services.UserServices.Interfaces;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;

namespace Foody.Application.Services.UserServices.Implements
{
    public class UserService : IUserService
    {
        private readonly FoodyAppContext _context;

        public UserService(FoodyAppContext context)
        {
            _context = context;
        }

        public UserResponseDto GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new UserFriendlyException($"Người dùng có id {id} không tồn tại");
            }
            return new UserResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public void UpdateUserInfo(UpdateUserDto input)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == input.Id);
            if (user == null)
            {
                throw new UserFriendlyException($"Người dùng có id {input.Id} không tồn tại");
            }
            user.UpdatedAt = DateTime.Now;
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.PhoneNumber = input.PhoneNumber;
            _context.SaveChanges();
        }
    }
}
