using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        #region quản lý đơn hàng nháp (giỏ hàng)

        //Thêm sản phẩm vào giỏ hàng
        public async Task<string> AddProductToCart(int productId)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = await _context.Orders.FirstOrDefaultAsync(c => c.UserId == currentUserId);
                    if (order == null)
                    {
                        await _context.Orders.AddAsync(new Order
                        {
                            UserId = currentUserId,
                            Status = OrderStatus.DRAFT,
                            PaymentMethod = PaymentMethod.COD
                        });
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var orderDetail = await _context.OrderDetails.Where(od => od.OrderId == order.Id && od.ProductId == productId).FirstOrDefaultAsync();
                        if (orderDetail == null)
                        {
                            await _context.OrderDetails.AddAsync(new OrderDetail
                            {
                                ProductId = productId,
                                Quantity = 1,
                                OrderId = order.Id
                            });
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            orderDetail.Quantity += 1;
                            await _context.SaveChangesAsync();
                        }
                    }
                    await transaction.CommitAsync();
                    return "Thêm thành công";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return "Lỗi: " + ex.Message;
                }
            }
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public async Task RemoveProductFromCart(int productId)
        {
            var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(od => od.Id == productId);

            if (orderDetail == null)
            {
                throw new UserFriendlyException("Không tìm thấy sản phẩm trong đơn nháp");
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
        }
        //Lấy danh sách sản phẩm trong giỏ hàng
        public async Task<CartResponse> GetCartByUserId()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var shoppingCart = await (from ord in _context.Orders
                                      join od in _context.OrderDetails on ord.Id equals od.OrderId
                                      join product in _context.Products on od.ProductId equals product.Id
                                      join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                                      join pro in _context.Promotions on pp.PromotionId equals pro.Id
                                      where ord.UserId == userId && ord.Status == OrderStatus.DRAFT
                                      && product.IsActived == true && product.IsDeleted == false
                                      && pp.IsActive == true
                                      group new { ord, od, product, pro } by ord.Id into grouped
                                      select new CartResponse
                                      {
                                          OrderId = grouped.Key,
                                          TotalPrice = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
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
            if (shoppingCart == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào trong giỏ hàng");
            }
            return shoppingCart;
        }

        #endregion

        //Đổi trạng thái đơn hàng
        public async Task UpdateOrderStatus(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            var status = (int)order.Status;
            
            switch (status)
            {
                case 1:
                    order.Status = OrderStatus.INPROGRESS;
                    break;
                case 2:
                    order.Status = OrderStatus.SUCCESS;
                    break;
            }
            await _context.SaveChangesAsync();
        }

        public async Task CancelOrderStatus(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if(order == null) {
                throw new UserFriendlyException($"Đơn hàng có ID {orderId} không tồn tại");
            }
            else
            {
                order.Status = OrderStatus.CANCELED;
                await _context.SaveChangesAsync();
            }
        }

        public Task<OrderResponseDto> GetOrderByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
