using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Foody.Application.Services.OrderServices.Implements
{
    public class OrderService : IOrderService
    {
        private readonly FoodyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderService(FoodyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }



        //Đổi trạng thái đơn hàng
        public async Task UpdateOrderStatus(UpdateOrderStatusDto input)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == input.Id);
            if (order == null)
            {
                throw new UserFriendlyException("Không tìm thấy đơn hàng");
            }
            order.Status = input.newStatus;
            await _context.SaveChangesAsync();
        }

        public async Task CancelOrderStatus(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new UserFriendlyException($"Đơn hàng có ID {orderId} không tồn tại");
            }
            else
            {
                order.Status = OrderStatus.CANCELED;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<OrderResponseDto> GetPendingOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.INPROGRESS
                               && product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
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
                               }).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được xử lý");
            }
            return order;
        }
        public async Task<OrderResponseDto> GetShippingOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.SHIPPING
                               && product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
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
                               }).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được vận chuyển");
            }
            return order;
        }
        public async Task<OrderResponseDto> GetSuccessOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.SUCCESS
                               && product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
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
                               }).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }
        public async Task<OrderResponseDto> GetCanceledOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.CANCELED
                               && product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
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
                               }).FirstOrDefaultAsync();
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }

        public async Task CreateOrder(CreateOrderDto input)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newOrder = new Order
                {
                    PaymentMethod = input.PaymentMethod,
                    Status = OrderStatus.INPROGRESS,
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                var newOrderdetail = new OrderDetail { 
                    ProductId = input.ProductId,
                    Quantity = input.Quantity,
                    OrderId = newOrder.Id
                };
                await _context.OrderDetails.AddAsync(newOrderdetail);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Lỗi khi tạo đơn hàng: " + ex.Message);
            }

        }
       
        public async Task CreateOrderFromCart(CreateOrderFromCartDto input)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var cart = _context.Carts.Include(c => c.ProductCarts).FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                throw new UserFriendlyException("Giỏ hàng không tồn tại");
            }
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var order = new Order { UserId = userId, CreatedAt = DateTime.Now, CreatedBy = userId };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                foreach (var productCart in cart.ProductCarts)
                {
                    var orderDetail = new OrderDetail { OrderId = order.Id, ProductId = productCart.ProductId, Quantity = productCart.Quantity };
                    await _context.OrderDetails.AddAsync(orderDetail);
                    await _context.SaveChangesAsync();
                }

                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new UserFriendlyException(ex.Message);
            }
        }
        //Cần các chức năng như:
        //- Thanh toán từ giỏ hàng: thanh toán tất cả sản phẩm trong giỏ hàng, thanh toán 1 hoặc nhiều sản phẩm trong giỏ hàng
        //- Thanh toán sản phẩm đặt mua trực tiếp từ màn hình home
    }
}
