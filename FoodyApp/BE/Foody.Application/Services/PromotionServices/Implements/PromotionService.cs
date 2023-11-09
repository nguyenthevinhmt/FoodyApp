using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Foody.Share.Shared.FilterDto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foody.Application.Services.PromotionServices.Implements
{
    public class PromotionService : IPromotionService
    {
        private readonly FoodyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PromotionService(FoodyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task CreatePromotion(CreatePromotionDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            await _context.Promotions.AddAsync(new Promotion
            {
                Name = input.Name,
                DiscountPercent = input.DiscountPercent,
                Description = input.Description,
                PromotionCode = input.PromotionCode,
                CreatedAt = DateTime.Now,
                IsActive = input.IsActive,
                StartTime = input.StartTime,
                EndTime = input.EndTime,
                CreatedBy = currentUserId
            });
            await _context.SaveChangesAsync();
        }

        public async Task Update(UpdatePromotionDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == input.Id);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Chương trình khuyến mại có id {input.Id} không tồn tại!");
            }
            promotion.UpdatedAt = DateTime.Now;
            promotion.IsActive = input.IsActive;
            promotion.Description = input.Description;
            promotion.DiscountPercent = input.DiscountPercent;
            promotion.StartTime = input.StartTime;
            promotion.EndTime = input.EndTime;
            promotion.Name = input.Name;
            promotion.UpdatedAt = DateTime.Now;
            promotion.UpdateBy = currentUserId;

            await _context.SaveChangesAsync();
        }

        public async Task<PromotionResponseDto> GetById(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Chương trình khuyến mại có id {id} không tồn tại!");
            }
            else
            {
                return new PromotionResponseDto
                {
                    Id = promotion.Id,
                    PromotionCode = promotion.PromotionCode,
                    StartTime = promotion.StartTime,
                    EndTime = promotion.EndTime,
                    CreatedAt = promotion.CreatedAt,
                    CreatedBy = promotion.CreatedBy,
                    Description = promotion.Description,
                    DiscountPercent = promotion.DiscountPercent,
                    IsActive = promotion.IsActive,
                    Name = promotion.Name
                };
            }
        }

        public async Task<PageResultDto<PromotionResponseDto>> GetPromotionPaging(PromotionFilterDto input)
        {
            var query = _context.Promotions.AsQueryable();

            query = query.Where(p =>
                (string.IsNullOrEmpty(input.Keyword) || p.PromotionCode.ToLower().Contains(input.Keyword.ToLower()))
                && ((p.CreatedAt >= input.StartTime && p.CreatedAt <= input.EndTime)) && p.IsDeleted == false);

            var totalItem = await query.CountAsync();

            var queryList = await query
                .OrderByDescending(p => p.StartTime)
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            var items = queryList.Select(p => new PromotionResponseDto
            {
                Id = p.Id,
                PromotionCode = p.PromotionCode,
                Name = p.Name,
                DiscountPercent= p.DiscountPercent,
                StartTime = p.StartTime,
                EndTime = p.EndTime,
                CreatedAt = p.CreatedAt,
                CreatedBy = p.CreatedBy,
                Description = p.Description,
                IsActive = p.IsActive,
            }).ToList();
            var pageResult = new PageResultDto<PromotionResponseDto>
            {
                Item = items,
                TotalItem = totalItem
            };
            return pageResult;
        }

        public async Task Delete(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Chương trình khuyến mại có id {id} không tồn tại!");
            }
            promotion.IsDeleted = true;
            promotion.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
