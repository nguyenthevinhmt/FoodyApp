using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CartServices.Dtos
{
    public class UpdateCartDto
    {
        public int productId { get; set; }
        public int quantity { get; set; }
    }
}
