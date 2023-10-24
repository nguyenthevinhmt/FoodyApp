using Foody.Application.Services.VnpayService.Dtos;
using Microsoft.AspNetCore.Http;

namespace Foody.Application.Services.VnpayService.Interfaces
{
    public interface IVnpayService
    {
        public string CreatePaymentUrl(PaymentInformationDto model, HttpContext context);
        PaymentResponseDto PaymentExecute(IQueryCollection collections);
    }
}
