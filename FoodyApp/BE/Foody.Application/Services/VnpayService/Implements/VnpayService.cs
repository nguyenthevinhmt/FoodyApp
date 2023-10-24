using Foody.Application.Services.VnpayService.Dtos;
using Foody.Application.Services.VnpayService.Interfaces;
using Foody.Domain.Constants;
using Foody.Infrastructure.Persistence;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;


namespace Foody.Application.Services.VnpayService.Implements
{
    public class VnpayService : IVnpayService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FoodyAppContext _context;

        public VnpayService(IConfiguration configuration, FoodyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public string CreatePaymentUrl(PaymentInformationDto model, HttpContext context)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var userId = CommonUtils.GetUserId(_httpContextAccessor);

            var order = (from ord in _context.Orders
                         join orderDetail in _context.OrderDetails on ord.Id equals orderDetail.OrderId
                         join product in _context.Products on orderDetail.ProductId equals product.Id
                         join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                         join pro in _context.Promotions on pp.PromotionId equals pro.Id
                         where ord.UserId == userId && ord.Id == model.OrderId
                         select new
                         {
                             Id = ord.Id,
                             OrderType = product.CategoryId,
                             TotalAmount = orderDetail.Quantity * (product.ActualPrice - product.ActualPrice * pro.DiscountPercent / 100),
                             CustomerName = _context.Users.Where(c => c.Id == userId).Select(c => c.FullName).FirstOrDefault(),
                             OrderDescription = "Thanh toán đơn hàng tại Foody App"
                         }).FirstOrDefault();

            pay.AddRequestData("vnp_Version", "2.1.0");
            pay.AddRequestData("vnp_Command", "pay");
            pay.AddRequestData("vnp_TmnCode", "4ZDNL1XX");
            pay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", order.Id.ToString());
            pay.AddRequestData("vnp_OrderType", order.OrderType.ToString());
            pay.AddRequestData("vnp_ReturnUrl", "http://localhost:5010/api/Vnpay/PaymentCallback");
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "ZOKMXYHQSRKISYPUGEHDRBGXQKEYPEMZ");

            return paymentUrl;
        }

        public PaymentResponseDto PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, "ZOKMXYHQSRKISYPUGEHDRBGXQKEYPEMZ");
            if (response.Success == true && response.VnPayResponseCode == "00")
            {
                var order = _context.Orders.FirstOrDefault(c => c.Id == int.Parse(response.OrderInfo));
                order.Status = OrderStatus.PAID;
                _context.SaveChanges();
            }
            return response;
        }
    }
}
