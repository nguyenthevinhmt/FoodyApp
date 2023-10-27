using Foody.Application.Services.OrderServices.Dtos;
using Foody.Application.Services.VnpayService.Dtos;
using Microsoft.AspNetCore.Http;

namespace Foody.Application.Services.VnpayService.Interfaces
{
    public interface IVnpayService
    {
        public string CreatePaymentUrl(PaymentInformationDto model, HttpContext context);
    }
}
