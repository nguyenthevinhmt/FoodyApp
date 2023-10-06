using Foody.Domain.Constants;
using Foody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderResponseDto
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int Status { get; set; }
        public int ProductCartId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
