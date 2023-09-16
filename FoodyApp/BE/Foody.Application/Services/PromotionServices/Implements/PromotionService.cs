﻿using Foody.Application.Exceptions;
using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
using Foody.Application.Shared.FilterDto;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Foody.Application.Services.PromotionServices.Implements
{
    public class PromotionService : IPromotionService
    {
        private readonly FoodyAppContext _context;

        public PromotionService(FoodyAppContext context)
        {
            _context = context;
        }
        public async Task CreatePromotion(CreatePromotionDto input)
        {
            await _context.Promotions.AddAsync(new Promotion
            {
                Name = input.Name,
                DiscountPercent = input.DiscountPercent,
                Description = input.Description,
                PromotionCode = input.PromotionCode,
                CreatedAt = DateTime.Now,
                IsActive = input.IsActive,
                StartTime = input.StartTime,
                EndTime = input.EndTime
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePromotion(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Phiếu giảm giá có id={id} không tồn tại!");
            }
            promotion.IsActive = false;
            promotion.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }

        public async Task<PromotionResponseDto> getPromotionById(int id)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Phiếu giảm giá có id={id} không tồn tại!");
            }
            return new PromotionResponseDto
            {
                Id = id,
                Name = promotion.Name,
                PromotionCode = promotion.PromotionCode,
                DiscountPercent = promotion.DiscountPercent,
                Description = promotion.Description,
                StartTime = promotion.StartTime,
                EndTime = promotion.EndTime,
                IsActive=promotion.IsActive,
            };

        }

        public async Task<PageResultDto<PromotionResponseDto>> getPromotionPaging(PromotionFilterDto input)
        {
            var query = _context.Promotions.AsQueryable();

            query = query.Where(p =>
                (string.IsNullOrEmpty(input.Keyword) || p.PromotionCode.ToLower().Contains(input.Keyword.ToLower()))
                && ((p.CreatedAt >= input.StartTime && p.CreatedAt <= input.EndTime)));

            var totalItem = await query.CountAsync();

            var queryList = await query
                .OrderByDescending(p => p.StartTime)
                .Skip((input.PageIndex - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            var items = queryList.Select(p => new PromotionResponseDto
            {
                PromotionCode = p.PromotionCode,
                Name = p.Name,
                DiscountPercent= p.DiscountPercent,
                StartTime = p.StartTime,
                EndTime = p.EndTime,

            }).ToList();
            var pageResult = new PageResultDto<PromotionResponseDto>
            {
                Item = items,
                TotalItem = totalItem
            };
            return pageResult;
        }

        public async Task UpdatePromotion(UpdatePromotionDto input)
        {
            var promotion = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == input.Id);
            if (promotion == null)
            {
                throw new UserFriendlyException($"Phiếu giảm giá có id = {input.Id} không tồn tại");
            }

            promotion.PromotionCode = input.PromotionCode;
            promotion.Name = input.Name;
            promotion.Description = input.Description;
            promotion.DiscountPercent = input.DiscountPercent;
            promotion.StartTime = input.StartTime;
            promotion.EndTime = input.EndTime;
            promotion.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
        }
    }
}
