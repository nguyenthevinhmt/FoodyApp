using Foody.Application.Services.DashboardServices.Dtos;
using Foody.Application.Services.DashboardServices.Interfaces;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Shared.FilterDto;
using MailKit.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.DashboardServices.Implements
{
    public class DashboardService : IDashboardService
    {

        private readonly FoodyAppContext _context;

        public DashboardService(FoodyAppContext context)
        {
            _context = context;
        }

        public async Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatistics(DashboardFilterDto input)
        {
            var query = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true && ord.Status == OrderStatus.SUCCESS
                               && ord.UpdatedAt >= input.StartDate && ord.UpdatedAt <= input.EndDate
                               group new { ord, od, product, pp, pro } by ord.Id into grouped
                               select new DashboardResponseDto
                               {
                                   Id = grouped.Key,
                                   UpdatedDate = grouped.Select(c => c.ord.UpdatedAt).FirstOrDefault(),
                                   OrderRevenue = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   Products = grouped.Select(p => new InfoProductCartDto
                                   {
                                       Id = p.product.Id,
                                       Name = p.product.Name,
                                       ActualPrice = p.product.ActualPrice - (p.product.ActualPrice * p.pro.DiscountPercent / 100),
                                       CategoryId = p.product.CategoryId,
                                       Description = p.product.Description,
                                       ProductImageUrl = p.product.ProductImages.Select(o => o.ProductImageUrl).FirstOrDefault(),
                                       Quantity = p.od.Quantity,
                                       CreateBy = p.product.CreatedBy,
                                       Price = p.product.Price,
                                       IsActive = p.product.IsActived,
                                   }).ToList(),
                               }).ToListAsync();

            var result = new DashboardPageResultDto<DashboardResponseDto>();

            result.TotalItem = query.Count();
            result.TotalRevenue = query.Sum(q => q.OrderRevenue);

            query = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            result.Item = query;

            return result;
        }

        public async Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatisticsByDay(DashboardFilterByDayDto input)
        {
            var query = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true && ord.Status == OrderStatus.SUCCESS
                               && ord.UpdatedAt.Year == input.Year && ord.UpdatedAt.Month == input.Month && ord.UpdatedAt.Day == input.Day
                               group new { ord, od, product, pp, pro } by ord.Id into grouped
                               select new DashboardResponseDto
                               {
                                   Id = grouped.Key,
                                   UpdatedDate = grouped.Select(c => c.ord.UpdatedAt).FirstOrDefault(),
                                   OrderRevenue = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   Products = grouped.Select(p => new InfoProductCartDto
                                   {
                                       Id = p.product.Id,
                                       Name = p.product.Name,
                                       ActualPrice = p.product.ActualPrice - (p.product.ActualPrice * p.pro.DiscountPercent / 100),
                                       CategoryId = p.product.CategoryId,
                                       Description = p.product.Description,
                                       ProductImageUrl = p.product.ProductImages.Select(o => o.ProductImageUrl).FirstOrDefault(),
                                       Quantity = p.od.Quantity,
                                       CreateBy = p.product.CreatedBy,
                                       Price = p.product.Price,
                                       IsActive = p.product.IsActived,
                                   }).ToList(),
                               }).ToListAsync();

            var result = new DashboardPageResultDto<DashboardResponseDto>();

            result.TotalItem = query.Count();
            result.TotalRevenue = query.Sum(q => q.OrderRevenue);

            query = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            result.Item = query;

            return result;
        }

        public async Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatisticsByMonth(DashboardFilterByMonthDto input)
        {
            var query = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true && ord.Status == OrderStatus.SUCCESS
                               && ord.UpdatedAt.Year == input.Year && ord.UpdatedAt.Month == input.Month
                               group new { ord, od, product, pp, pro } by ord.Id into grouped
                               select new DashboardResponseDto
                               {
                                   Id = grouped.Key,
                                   UpdatedDate = grouped.Select(c => c.ord.UpdatedAt).FirstOrDefault(),
                                   OrderRevenue = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   Products = grouped.Select(p => new InfoProductCartDto
                                   {
                                       Id = p.product.Id,
                                       Name = p.product.Name,
                                       ActualPrice = p.product.ActualPrice - (p.product.ActualPrice * p.pro.DiscountPercent / 100),
                                       CategoryId = p.product.CategoryId,
                                       Description = p.product.Description,
                                       ProductImageUrl = p.product.ProductImages.Select(o => o.ProductImageUrl).FirstOrDefault(),
                                       Quantity = p.od.Quantity,
                                       CreateBy = p.product.CreatedBy,
                                       Price = p.product.Price,
                                       IsActive = p.product.IsActived,
                                   }).ToList(),
                               }).ToListAsync();

            var result = new DashboardPageResultDto<DashboardResponseDto>();

            result.TotalItem = query.Count();
            result.TotalRevenue = query.Sum(q => q.OrderRevenue);

            query = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            result.Item = query;

            return result;
        }
        public async Task<PageResultDto<DashboardResponseTopProductsDto>> GetTopProducts(DashboardProductsFilterDto input)
        {
            var Group = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join p in _context.Products on od.ProductId equals p.Id
                               join pi in _context.ProductImages on p.Id equals pi.ProductId
                               join pp in _context.ProductPromotions on p.Id equals pp.ProductId
                               join prom in _context.Promotions on pp.PromotionId equals prom.Id
                               where ord.Status == OrderStatus.SUCCESS
                               select new
                               {
                                   Id = od.Id,
                                   OrderStatus = ord.Status,
                                   Quantity = od.Quantity,
                                   ProductId = p.Id,
                                   Name = p.Name,
                                   Price = p.Price,
                                   ActualPrice = p.ActualPrice - (p.ActualPrice * prom.DiscountPercent / 100),
                                   TotalPrice = (p.ActualPrice - (p.ActualPrice * prom.DiscountPercent / 100)) * od.Quantity,
                                   Description = p.Description,
                                   CategoryId = p.CategoryId,
                                   IsActived = p.IsActived,
                                   CreatedAt = p.CreatedAt,
                                   ProductImageUrl = pi.ProductImageUrl
                               }).ToListAsync();

            var query = (Group.GroupBy(g => g.ProductId)
                                .Select(r => new DashboardResponseTopProductsDto
                                {
                                    Id = r.Key,
                                    Name = r.FirstOrDefault()?.Name,
                                    Price = (double)(r.FirstOrDefault()?.Price),
                                    Description = r.FirstOrDefault()?.Description,
                                    CategoryId = (int)(r.FirstOrDefault()?.CategoryId),
                                    IsActived = (bool)(r.FirstOrDefault()?.IsActived),
                                    CreatedAt = (DateTime)(r.FirstOrDefault()?.CreatedAt),
                                    ProductImageUrl = r.FirstOrDefault()?.ProductImageUrl,
                                    TotalQuantity = r.Sum(c => c.Quantity),
                                    TotalRevenue = r.Sum(c => c.TotalPrice)
                                })).ToList()
                                .OrderByDescending(q => q.TotalQuantity).Take(input.amount).ToList();

            var result = new PageResultDto<DashboardResponseTopProductsDto>();

            result.TotalItem = query.Count();
            query = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            result.Item = query;

            return result;

        }
        
    }
}
