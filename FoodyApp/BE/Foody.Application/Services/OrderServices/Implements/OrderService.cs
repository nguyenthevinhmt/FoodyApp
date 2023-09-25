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
        public async Task<string> AddProductToDraftOrder(int productId)
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
        public async Task RemoveProductFromDraftOrder(int productId)
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
        public async Task<List<DraftOrderResponse>> GetDraftOrdersByUserId()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var query = await (from order in _context.Orders
                               where order.UserId == userId
                               join orderDetail in _context.OrderDetails on order.Id equals orderDetail.OrderId
                               select new DraftOrderResponse
                               {
                                   OrderId = orderDetail.OrderId,
                                   Product = _context.Products.Where(c => c.Id == orderDetail.ProductId).FirstOrDefault(),
                                   ProductId = orderDetail.ProductId,
                                   Quantity = orderDetail.Quantity,
                                   TotalPrice = orderDetail.Quantity * orderDetail.Product.ActualPrice
                               }).ToListAsync();
            //var listItem = new List<DraftOrderResponse>();
            //foreach (var orderDetail in query)
            //{
            //    var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == orderDetail.ProductId);
            //    listItem.Add(new DraftOrderResponse
            //    {
            //        Id = orderDetail.Id,
            //        OrderId = orderDetail.OrderId,
            //        Order = orderDetail.Order,
            //        Product = product,
            //        ProductId = orderDetail.ProductId,
            //        Quantity = orderDetail.Quantity,
            //        TotalPrice = orderDetail.Quantity * orderDetail.Product.ActualPrice
            //    });
            //};
            return query;

        }
        #endregion
    }
}
