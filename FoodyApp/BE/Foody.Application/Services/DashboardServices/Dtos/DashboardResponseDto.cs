using Foody.Application.Services.OrderServices.Dtos;
using Foody.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.DashboardServices.Dtos
{
    public class DashboardResponseDto
    {
        public int Id { get; set; }
        public double OrderRevenue { get; set; } //doanh thu theo đơn hàng
        public List<InfoProductCartDto> Products { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
