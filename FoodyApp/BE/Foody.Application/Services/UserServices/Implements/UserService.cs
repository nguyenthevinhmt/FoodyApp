using Foody.Application.Services.UserServices.Dtos;
using Foody.Application.Services.UserServices.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Foody.Application.Services.UserServices.Implements
{
    public class UserService : IUserService
    {
        private readonly FoodyAppContext _context;

        public UserService(FoodyAppContext context)
        {
            _context = context;
        }

        public async Task AddAddressForUser(CreateAddressDto input)
        {
            await _context.UserAddresses.AddAsync(new UserAddress
            {
                AddressType = input.AddressType,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = input.UserId,
                Province = input.Province,
                UserId = input.UserId,
                DetailAddress = input.DetailAddress,
                District = input.District,
                Notes = input.Notes,
                StreetAddress = input.StreetAddress,
                Ward = input.Ward
            });
            await _context.SaveChangesAsync();
        }
        public async Task<PageResultDto<AddressResponseDto>> GetAddressForUserPaging(AddressFilterDto input)
        {
            var query = _context.UserAddresses.AsQueryable();
            query = query.Where(a => a.UserId == input.UserId);

            var totalItem = await query.CountAsync();

            var listItem = await query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize)
                .Select(address => new AddressResponseDto
                {
                    Id = address.Id,
                    Province = address.Province,
                    District = address.District,
                    Ward = address.Ward,
                    StreetAddress = address.StreetAddress,
                    DetailAddress = address.DetailAddress,
                    Notes = address.Notes,
                    AddressType = address.AddressType,
                    CreatedAt = address.CreatedAt,
                    CreatedBy = address.CreatedBy,
                    UpdatedAt = address.UpdatedAt,
                    UpdateBy = address.UpdateBy
                }).ToListAsync();

            var pageResult = new PageResultDto<AddressResponseDto>
            {
                Item = listItem,
                TotalItem = totalItem
            };
            return pageResult;
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

        public async Task UpdateAddressForUser(UpdateAddressDto input)
        {
            var address = await _context.UserAddresses.FirstOrDefaultAsync(a => a.Id == input.Id);
            if (address == null)
            {
                throw new UserFriendlyException($"Địa chỉ có id = {input.Id} không tồn tại.");
            }
            address.UserId = input.UserId;
            address.Province = input.Province;
            address.District = input.District;
            address.Ward = input.Ward;
            address.StreetAddress = input.StreetAddress;
            address.DetailAddress = input.DetailAddress;
            address.Notes = input.Notes;
            address.AddressType = input.AddressType;
            address.UpdatedAt = DateTime.Now;
            address.UpdateBy = input.UserId;

            await _context.SaveChangesAsync();
        }

        public void UpdateUserInfo(UpdateUserDto input)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == input.Id);
            if (user == null)
            {
                throw new UserFriendlyException($"Người dùng có id {input.Id} không tồn tại");
            }
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.PhoneNumber = input.PhoneNumber;
            _context.SaveChanges();
        }
    }
}
