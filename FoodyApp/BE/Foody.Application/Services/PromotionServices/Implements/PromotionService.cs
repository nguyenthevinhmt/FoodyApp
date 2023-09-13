using Foody.Application.Services.PromotionServices.Dtos;
using Foody.Application.Services.PromotionServices.Interfaces;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;

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
            _context.Promotions.Add(new Promotion
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
            _context.SaveChanges();
        }
    }
}
