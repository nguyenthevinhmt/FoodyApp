using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class AdminOrderDto : OrderResponseDto
    {
        public string CustomerFullName { get;set; }
        public int OrderStatus { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
