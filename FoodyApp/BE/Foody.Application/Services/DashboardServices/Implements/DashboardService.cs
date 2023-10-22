using Foody.Application.Services.DashboardServices.Dtos;
using Foody.Application.Services.DashboardServices.Interfaces;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Shared.FilterDto;
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
                               && pp.IsActive == true && ord.Status == 4
                               && ord.CreatedAt >= input.StartDate && ord.CreatedAt <= input.EndDate
                               group new { ord, od, product, pp, pro } by ord.Id into grouped
                               select new DashboardResponseDto
                               {
                                   Id = grouped.Key,
                                   CreatedDate = grouped.Select(c => c.ord.CreatedAt).FirstOrDefault(),
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
    }
}
