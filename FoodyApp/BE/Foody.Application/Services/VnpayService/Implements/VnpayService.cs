using Foody.Application.Services.VnpayService.Dtos;
using Foody.Application.Services.VnpayService.Interfaces;
using Foody.Domain.Constants;
using Foody.Infrastructure.Persistence;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Foody.Application.Services.VnpayService.Implements
{
    public class VnpayService : IVnpayService
    {
        private readonly AppSettings _appSettings;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly FoodyAppContext _context;

        public VnpayService(IOptions<AppSettings> options, IConfiguration configuration, FoodyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _configuration = configuration;
        }
        public string CreatePaymentUrl(PaymentInformationDto model, HttpContext context)
        {
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var userId = CommonUtils.GetUserId(_httpContextAccessor);

            var orderTemp = (from ord in _context.Orders
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
                         }).ToList();

            var order = new
            {
                Id = orderTemp.First().Id,
                OrderType = orderTemp.First().OrderType,
                TotalAmount = orderTemp.Sum(o => o.TotalAmount),
                CustomerName = orderTemp.First().CustomerName,
                OrderDescription = orderTemp.First().OrderDescription,
            };

            pay.AddRequestData("vnp_Version", _appSettings.Version);
            pay.AddRequestData("vnp_Command", _appSettings.Command);
            pay.AddRequestData("vnp_TmnCode", _appSettings.TmnCode);
            pay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _appSettings.CurrCode);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _appSettings.Locale);
            pay.AddRequestData("vnp_OrderInfo", order.Id.ToString());
            pay.AddRequestData("vnp_OrderType", order.OrderType.ToString());
            pay.AddRequestData("vnp_ReturnUrl", "http://localhost:5010/api/VnpayCallback/PaymentCallback");
            pay.AddRequestData("vnp_TxnRef", tick);
            pay.AddRequestData("vnp_BankCode", _appSettings.BankCode);

            var paymentUrl =
                pay.CreateRequestUrl(_appSettings.BaseUrl, _appSettings.HashSecret);

            return paymentUrl;
        }
    }
}
