using Foody.Domain.Constants;
using Foody.Share.Exceptions;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class UpdateOrderStatusDto
    {
        public int Id { get; set; }
        [IntegerRange(AllowableValues = new int[] { OrderStatus.ACCEPTED, OrderStatus.INPROGRESS, OrderStatus.SHIPPING, OrderStatus.SUCCESS, OrderStatus.CANCELED })]
        public int newStatus { get; set; }
    }
}
