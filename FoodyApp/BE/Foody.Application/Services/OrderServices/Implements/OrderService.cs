using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Application.Services.ProductServices.Dtos;
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

        //Xóa sản phẩm khỏi đơn hàng nháp
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
        //Lấy danh sách sản phẩm trong đơn hàng nháp
        public async Task<CartResponse> GetCartByUserId()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var shoppingCart = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .ThenInclude(img => img.ProductImages)
                .Where(o => o.UserId == userId && o.Status == OrderStatus.DRAFT)
                .FirstOrDefaultAsync();
            if (shoppingCart == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào trong giỏ hàng");
            }
            var shoppingCartDto = new CartResponse
            {
                OrderId = shoppingCart.Id,
                TotalPrice = shoppingCart.OrderDetails.Sum(od => od.Quantity * od.Product.ActualPrice),
                Products = shoppingCart.OrderDetails.Select(od => new ProductResponseDto
                {
                    Id = od.ProductId,
                    Name = od.Product.Name,
                    Price = od.Product.Price,
                    ActualPrice = od.Product.ActualPrice,
                    CategoryId = od.Product.CategoryId,
                    Description = od.Product.Description,
                    ProductImageUrl = od.Product.ProductImages.Select(o => o.ProductImageUrl).FirstOrDefault(),
                    CreateBy = od.Product.CreatedBy,
                    IsActive = od.Product.IsActived,
                    IsDeleted = od.Product.IsDeleted   
                }).ToList()
            };
            return shoppingCartDto;
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
