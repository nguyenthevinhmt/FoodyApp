using Foody.Application.Services.DashboardServices.Dtos;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Share.Shared.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.DashboardServices.Interfaces
{
    public interface IDashboardService
    {
        /// <summary>
        /// số lượng, tổng doanh thu đơn đã giao thành công theo thời gian
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatistics(DashboardFilterDto input);
        /// <summary>
        /// số lượng, tổng doanh thu đơn đã giao thành công theo ngày
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatisticsByDay(DashboardFilterByDayDto input);
        /// <summary>
        /// số lượng, tổng doanh thu đơn đã giao thành công theo tháng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<DashboardPageResultDto<DashboardResponseDto>> GetOrderStatisticsByMonth(DashboardFilterByMonthDto input);
        /// <summary>
        /// thống kê số lượng những sản phẩm được mua nhiều nhất
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PageResultDto<DashboardResponseTopProductsDto>> GetTopProducts(DashboardProductsFilterDto input);
    }
}
