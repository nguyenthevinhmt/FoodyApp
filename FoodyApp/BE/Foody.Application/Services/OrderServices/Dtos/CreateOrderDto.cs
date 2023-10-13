using Foody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CreateOrderDto
    {
        public int ProductId { get; set; }
        public int PaymentMethod { get; set; }
        public int Quantity { get; set; }
        //public UserAddressDto UserAddress { get; set; }
        public int AddressType { get; set; }
    }
}
