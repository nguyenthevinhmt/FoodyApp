namespace Foody.Application.Services.VnpayService.Dtos
{
    public class PaymentResponseDto
    {
        public string OrderInfo { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public int PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
