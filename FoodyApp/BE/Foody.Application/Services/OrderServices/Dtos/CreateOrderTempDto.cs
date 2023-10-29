using Foody.Domain.Constants;
using Foody.Share.Exceptions;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateOrderTempDto
    {
        public int CartId { get; set; }
        public List<int> ProductId { get; set; }
        public int AddressType { get; set; }
        [IntegerRange(AllowableValues = new int[] { PaymentMethod.COD, PaymentMethod.VNPAY })]
        public int PaymentMethods { get; set; }
    }
}
