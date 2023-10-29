using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.DashboardServices.Dtos
{
    public class DashboardResponseTopProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string ProductImageUrl { get; set; }
        public bool IsActived { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalRevenue { get; set; }
    }
}
