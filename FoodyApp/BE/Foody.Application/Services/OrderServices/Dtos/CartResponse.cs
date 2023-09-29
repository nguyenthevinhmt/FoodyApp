using Foody.Application.Services.ProductServices.Dtos;
using Foody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class CartResponse
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<ProductResponseDto> Products { get; set; }
    }
}
