using Foody.Application.Services.VnpayService.Dtos;
using Foody.Application.Services.VnpayService.Interfaces;
using Foody.Infrastructure.Persistence;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;


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
        public string CreatePaymentUrl(PaymentInformationDto input, HttpContext context)
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var order = (from ord in _context.Orders
                        join orderDetail in _context.OrderDetails on ord.Id equals orderDetail.OrderId
                        join product in _context.Products on orderDetail.ProductId equals product.Id
                        join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                        join pro in _context.Promotions on pp.PromotionId equals pro.Id
                        where ord.UserId == userId && ord.Id == input.OrderId
                        select new {
                            Id = ord.Id,
                            OrderType = product.CategoryId,
                            TotalAmount = orderDetail.Quantity * (product.ActualPrice - product.ActualPrice * pro.DiscountPercent / 100),
                            CustomerName = _context.Users.Where(c => c.Id == userId).Select(c => c.FullName).FirstOrDefault(),
                            OrderDescription = "Thanh toán đơn hàng tại Foody App"
                        }).FirstOrDefault();


            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", (order.TotalAmount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{order.CustomerName} {order.OrderDescription} {order.TotalAmount}");
            pay.AddRequestData("vnp_OrderType", order.OrderType.ToString());
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public PaymentResponseDto PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            return response;
        }
    }
}
