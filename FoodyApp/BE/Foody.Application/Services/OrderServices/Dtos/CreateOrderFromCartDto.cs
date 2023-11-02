using Foody.Domain.Constants;
using Foody.Share.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateOrderFromCartDto
    {
        public int CartId { get; set; } 
        public List<int> ProductId { get; set; }

        [IntegerRange(AllowableValues = new int[] { PaymentMethod.COD, PaymentMethod.VNPAY })]
        public int PaymentMethods { get; set; }
        //public UserAddressDto UserAddress { get; set; }
        public int AddressType { get; set; }
    }
}
