﻿using Foody.Application.Services.ProductServices.Dtos;
using Foody.Domain.Entities;

namespace Foody.Application.Services.OrderServices.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public double TotalAmount { get; set; }
        public List<InfoProductCartDto> Products { get; set; }
    }
}
