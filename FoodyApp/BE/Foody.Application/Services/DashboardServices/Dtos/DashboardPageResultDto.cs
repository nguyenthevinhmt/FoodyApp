using Foody.Share.Shared.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.DashboardServices.Dtos
{
    public class DashboardPageResultDto<T> : PageResultDto<T>
    {
        public double TotalRevenue { get; set; } //tổng doanh thu
    }
}
