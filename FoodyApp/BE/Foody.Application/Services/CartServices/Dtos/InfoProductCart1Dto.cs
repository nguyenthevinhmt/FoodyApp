using Foody.Application.Services.ProductServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CartServices.Dtos
{
    public class InfoProductCart1Dto : ProductResponseDto
    {
        public int ProductCartId { get; set; }
        public int Quantity { get; set; }
    }
}
