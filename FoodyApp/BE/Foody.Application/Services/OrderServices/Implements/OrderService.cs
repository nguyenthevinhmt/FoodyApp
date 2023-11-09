using Foody.Application.Services.EmailServices;
using Foody.Application.Services.EmailServices.Dtos;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.OrderServices.Interfaces;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Migrations;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Foody.Share.Shared.FilterDto;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foody.Application.Services.OrderServices.Implements
{
    public class OrderService : IOrderService
    {
        private readonly FoodyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSenderService _mail;

        public OrderService(FoodyAppContext context, IHttpContextAccessor httpContextAccessor, IEmailSenderService mail)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mail = mail;
        }

        //Đổi trạng thái đơn hàng
        public async Task UpdateOrderStatus(UpdateOrderStatusDto input)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == input.OrderId);
            if (order == null)
            {
                throw new UserFriendlyException("Không tìm thấy đơn hàng");
            }
            order.Status = input.newStatus;
            order.UpdatedAt = DateTime.Now;
            order.UpdateBy = userId;
            if (input.newStatus == OrderStatus.INPROGRESS && order.Status == OrderStatus.INPROGRESS)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐANG ĐƯỢC XỬ LÝ]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    " 
                      
                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            else if (input.newStatus == OrderStatus.ACCEPTED && order.Status == OrderStatus.ACCEPTED)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐÃ ĐƯỢC XÁC NHẬN]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $"<p>Đơn hàng đang được vận chuyển và sẽ sớm được giao đến cho bạn.</p> \r\n" +
                        $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    "

                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            else if (input.newStatus == OrderStatus.SHIPPING && order.Status == OrderStatus.SHIPPING)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐANG ĐƯỢC GIAO ĐẾN BẠN]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $"<p>Đơn hàng đang được vận chuyển và sẽ sớm được giao đến cho bạn.</p> \r\n" +
                        $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    "

                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            else if (input.newStatus == OrderStatus.SUCCESS && order.Status == OrderStatus.SUCCESS)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐÃ ĐƯỢC GIAO THÀNH CÔNG]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $"<p>Đơn hàng đã được vận chuyển thành công, cảm ơn bạn đã mua hàng </p> \r\n"

                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
            else if (input.newStatus == OrderStatus.CANCELED && order.Status == OrderStatus.CANCELED)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐÃ ĐƯỢC HỦY]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $"<p>Đơn hàng đã bị hủy </p> \r\n"
                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }

            else if (input.newStatus == OrderStatus.PAID && order.Status == OrderStatus.PAID)
            {
                try
                {
                    // Lấy dịch vụ sendmailservice
                    MailContent content = new MailContent
                    {
                        To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                        Subject = $"[ĐƠN HÀNG {order.Id} ĐÃ ĐƯỢC THANH TOÁN]",
                        Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                        $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                        $"<p>Thanh toán đơn hàng thành công\r\n"

                    };
                    await _mail.SendMail(content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to send email: " + ex.Message);
                }
            }
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
                if (order.Status == OrderStatus.CANCELED)
                {
                    try
                    {
                        // Lấy dịch vụ sendmailservice
                        MailContent content = new MailContent
                        {
                            To = order.User.Email,
                            Subject = $"[ĐƠN HÀNG {order.Id} ĐÃ ĐƯỢC HỦY]",
                            Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                            $"    <h2>ĐƠN HÀNG #{order.Id}</h2>\r\n    " +
                            $"<p>Đơn hàng đã được hủy</p>" +
                            $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    "

                        };
                        await _mail.SendMail(content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<OrderResponseDto>> GetPendingOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.INPROGRESS
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true && od.IsDeleted == false
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được xử lý");
            }

            return order;
        }
        public async Task<List<OrderResponseDto>> GetAcceptOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.ACCEPTED
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true && od.IsDeleted == false
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được xử lý");
            }
            return order;
        }
        public async Task<List<OrderResponseDto>> GetShippingOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.SHIPPING
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true && od.IsDeleted == false
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
            if (order == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào đang được vận chuyển");
            }
            return order;
        }
        public async Task<List<OrderResponseDto>> GetSuccessOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.SUCCESS
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true && od.IsDeleted == false
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }
        public async Task<List<OrderResponseDto>> GetCanceledOrder()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Status == OrderStatus.CANCELED
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true && od.IsDeleted == false
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
            if (order == null)
            {
                throw new UserFriendlyException("Bạn chưa đặt sản phẩm nào");
            }
            return order;
        }
        public async Task<int> CreateOrder(CreateOrderDto input)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var userAddress = await _context.UserAddresses.FirstOrDefaultAsync(u => u.UserId == userId && u.AddressType == input.AddressType);
                if (userAddress == null)
                {
                    throw new UserFriendlyException("Địa chỉ của bạn không tồn tại, vui lòng cập nhật địa chỉ giao hàng");
                }
                var newOrder = new Order
                {
                    PaymentMethod = input.PaymentMethod,
                    Status = OrderStatus.INPROGRESS,
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    AddressType = userAddress.AddressType,
                    DetailAddress = userAddress.DetailAddress,
                    District = userAddress.District,
                    Notes = userAddress.Notes,
                    Province = userAddress.Province,
                    StreetAddress = userAddress.StreetAddress,
                    Ward = userAddress.Ward,
                    CreatedBy = userId,
                    IsPaid = false
                };
                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();
                var newOrderdetail = new OrderDetail
                {
                    ProductId = input.ProductId,
                    Quantity = input.Quantity,
                    OrderId = newOrder.Id
                };
                await _context.OrderDetails.AddAsync(newOrderdetail);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                if (newOrder.Status == OrderStatus.INPROGRESS)
                {
                    try
                    {
                        // Lấy dịch vụ sendmailservice
                        MailContent content = new MailContent
                        {
                            To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                            Subject = $"[ĐƠN HÀNG {newOrder.Id} ĐANG ĐƯỢC XỬ LÝ]",
                            Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                            $"    <h2>ĐƠN HÀNG #{newOrder.Id}</h2>\r\n    " +
                            $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    "

                        };
                        await _mail.SendMail(content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
                //trả về order vừa được tạo
                int newOrderId = newOrder.Id;
                return newOrderId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Lỗi khi tạo đơn hàng: " + ex.Message);
            }

        }
        public async Task<OrderResponseDto> GetOrderById(int id)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where ord.UserId == userId && ord.Id == id
                               && product.IsActived == true && product.IsDeleted == false && ord.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new OrderResponseDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   PaymentMethod = grouped.FirstOrDefault().ord.PaymentMethod,
                                   IsPaid = grouped.FirstOrDefault().ord.IsPaid,
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
                throw new UserFriendlyException("Đơn hàng không tồn tại");
            }
            return order;
        }
        public async Task<PageResultDto<AdminOrderDto>> GetAllOrders(OrderFilterDto input)
        {
            var query = await (from ord in _context.Orders
                               join od in _context.OrderDetails on ord.Id equals od.OrderId
                               join product in _context.Products on od.ProductId equals product.Id
                               join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                               join pro in _context.Promotions on pp.PromotionId equals pro.Id
                               where product.IsActived == true && product.IsDeleted == false
                               && pp.IsActive == true
                               group new { ord, od, product, pro } by ord.Id into grouped
                               select new AdminOrderDto
                               {
                                   Id = grouped.Key,
                                   TotalAmount = grouped.Sum(g => g.od.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.pro.DiscountPercent / 100)),
                                   CustomerFullName = grouped.Select(c => (c.ord.User.FullName).ToString()).FirstOrDefault(),
                                   OrderStatus = grouped.Select(c => c.ord.Status).FirstOrDefault(),
                                   CreatedDate = grouped.Select(c => c.ord.CreatedAt).FirstOrDefault(),
                                   UserAddress = grouped.Select(ud => new UserAddressDto
                                   {
                                       Province = ud.ord.Province,
                                       AddressType = ud.ord.AddressType,
                                       District = ud.ord.District,
                                       DetailAddress = ud.ord.DetailAddress,
                                       StreetAddress = ud.ord.StreetAddress,
                                       Notes = ud.ord.Notes,
                                       Ward = ud.ord.Ward,
                                   }).FirstOrDefault(),
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
                               }).Where(c => (!input.orderStatus.HasValue || c.OrderStatus == input.orderStatus)
                               && (input.Keyword == null || c.Id.ToString() == input.Keyword)
                               && (input.CreateDate == null || c.CreatedDate.Value.Date == input.CreateDate.Value.Date)).ToListAsync();

            var result = new PageResultDto<AdminOrderDto>();
            result.TotalItem = query.Count();
            query = query.Skip((input.PageIndex - 1) * input.PageSize).Take(input.PageSize).ToList();
            result.Item = query;
            return result;
        }

        //xóa đơn hàng được tạo trực tiếp từ sản phẩm trong trạng thái chờ
        public async Task DeleteOrder(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId && (o.Status == OrderStatus.INPROGRESS || o.Status == OrderStatus.ACCEPTED) && o.IsDeleted == false);
            if (order == null)
            {
                throw new UserFriendlyException($"Đơn hàng có id={orderId} không tồn tại hoặc không đủ điều kiện.");
            } 
            else
            {
                var orderDetail = await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderId == orderId);
                order.IsDeleted = true;
                orderDetail.IsDeleted = true;

                await _context.SaveChangesAsync();
            }

        }

        public async Task<int> CreateOrderFromCart(CreateOrderFromCartDto input)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var userAddress = _context.UserAddresses.FirstOrDefault(u => u.UserId == userId && u.AddressType == input.AddressType);
            if (userAddress == null)
            {
                throw new UserFriendlyException("Địa chỉ của bạn không tồn tại, vui lòng cập nhật địa chỉ giao hàng");
            }
            using var transaction = _context.Database.BeginTransaction();
            
            try
            {
                var cart = _context.Carts.Where(c => c.UserId == userId && c.Id == input.CartId).Include(c => c.ProductCarts).FirstOrDefault();

                var newOrder = new Order
                {
                    Status = OrderStatus.INPROGRESS,
                    AddressType = userAddress.AddressType,
                    DetailAddress = userAddress.DetailAddress,
                    District = userAddress.District,
                    Notes = userAddress.Notes,
                    PaymentMethod = input.PaymentMethods,
                    Province = userAddress.Province,
                    StreetAddress = userAddress.StreetAddress,
                    Ward = userAddress.Ward,
                    UserId = userId,
                    IsPaid = false,
                };

                await _context.Orders.AddAsync(newOrder);
                await _context.SaveChangesAsync();

                var orderDetail = new List<OrderDetail>();
                foreach (var c in cart.ProductCarts)
                {
                    foreach (var pi in input.ProductId)
                    {
                        if (c.ProductId == pi && c.IsDeleted == false)
                        {
                            orderDetail.Add(new OrderDetail
                            {
                                ProductId = pi,
                                Quantity = c.Quantity,
                                OrderId = newOrder.Id
                            });
                            c.IsDeleted = true;
                        }
                    }
                }
                await _context.OrderDetails.AddRangeAsync(orderDetail);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                /*
                if (newOrder.Status == OrderStatus.INPROGRESS)
                {
                    try
                    {
                        // Lấy dịch vụ sendmailservice
                        MailContent content = new MailContent
                        {
                            To = _context.Users.Where(c => c.Id == userId).Select(c => c.Email).FirstOrDefault(),
                            Subject = $"[ĐƠN HÀNG {newOrder.Id} ĐANG ĐƯỢC XỬ LÝ]",
                            Body = $"<h1>Foody - Ứng dụng đặt đồ ăn số 1 Việt Nam</h1>\r\n" +
                            $"    <h2>ĐƠN HÀNG #{newOrder.Id}</h2>\r\n    " +
                            $" <p>Vui lòng theo dõi gmail để biết tình trạng giao hàng.</p>\r\n    <p>\r\n    "

                        };
                        await _mail.SendMail(content);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to send email: " + ex.Message);
                    }
                }
                */
                //trả về order vừa được tạo
                int newOrderId = newOrder.Id;
                return newOrderId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new UserFriendlyException(ex.Message);
            }
        }

        public async Task OrderPaidSuccessResponse(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId && o.IsDeleted == false);
            if (order == null)
            {
                throw new UserFriendlyException($"Không tìm thấy đơn hàng với id={orderId}.");
            }
            order.IsPaid = true;
            await _context.SaveChangesAsync();
        }

        public async Task OrderFromCartFailResponse(OrderCartFailFilterDto input)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id ==  input.OrderId && o.IsDeleted == false);
            if (order == null)
            {
                throw new UserFriendlyException($"Không tìm thấy đơn hàng với id={input.OrderId}");
            }
            else
            {
                order.IsPaid = false;
                order.IsDeleted = true;
                await _context.SaveChangesAsync();

                //khi thanh toán thất bại sản phẩm vẫn ở trong giỏ hàng
                foreach (var p in input.ProductCartId)
                {
                    var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(pc => pc.Id == p);
                    productCart.IsDeleted = false;
                }
                await _context.SaveChangesAsync();

                var orderDetails = await _context.OrderDetails.Where(od => od.OrderId == input.OrderId).ToListAsync();
                foreach (var detail in orderDetails)
                {
                    detail.IsDeleted = false;
                }
                await _context.SaveChangesAsync();
            }
        }
    }
}
