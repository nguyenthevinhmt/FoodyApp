using Foody.Application.Services.VnpayService.Dtos;
using Microsoft.AspNetCore.Http;

namespace Foody.Application.Services.VnpayService.Interfaces
{
    public interface IVnpayService
    {
        string CreatePaymentUrl(PaymentInformationDto input, HttpContext context);
        PaymentResponseDto PaymentExecute(IQueryCollection collections);
    }
}
