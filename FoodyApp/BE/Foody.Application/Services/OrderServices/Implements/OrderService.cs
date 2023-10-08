using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        public async Task<CartResponseDto> GetCartByUserId()
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
                                      select new CartResponseDto
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

        public async Task<CartResponseDto> GetPendingOrder()
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
                               select new CartResponseDto
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
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được xử lý");
            }
            return order;
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đang xử lý
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<CartResponseDto> GetShippingOrder()
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
                               select new CartResponseDto
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
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được vận chuyển");
            }
            return order;
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đang xử lý
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<CartResponseDto> GetSuccessOrder()
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
                               select new CartResponseDto
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
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }
        /// <summary>
        /// Lấy tất cả đơn hàng đã hủy
        /// </summary>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public async Task<CartResponseDto> GetCanceledOrder()
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
                               select new CartResponseDto
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
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }
        //Cần các chức năng như:
        //- Thanh toán từ giỏ hàng: thanh toán tất cả sản phẩm trong giỏ hàng, thanh toán 1 hoặc nhiều sản phẩm trong giỏ hàng
        //- Thanh toán sản phẩm đặt mua trực tiếp từ màn hình home
    }
}
