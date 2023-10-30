using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderCartFailFilterDto
    {
        public int OrderId { get; set; }
        public List<int> ProductCartId { get; set; }
    }
}
