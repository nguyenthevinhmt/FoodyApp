using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.OrderServices.Dtos;

namespace Foody.Application.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Hủy đơn hàng
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task CancelOrderStatus(int orderId);
        /// <summary>
        /// Lấy đơn hàng ở trạng thái khởi tạo
        /// </summary>
        /// <returns></returns>
        public Task<OrderResponseDto> GetPendingOrder();
        /// <summary>
        /// Lấy đơn hàng ở trạng thái đang giao
        /// </summary>
        /// <returns></returns>
        public Task<OrderResponseDto> GetShippingOrder();
        /// <summary>
        /// Lấy đơn hàng ở trạng thái giao thành công
        /// </summary>
        /// <returns></returns>
        public Task<OrderResponseDto> GetSuccessOrder();
        /// <summary>
        /// Lấy tất cả đơn đã hủy
        /// </summary>
        /// <returns></returns>
        public Task<OrderResponseDto> GetCanceledOrder();
        /// <summary>
        /// Tạo mới đơn hàng trực tiếp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task CreateOrder(CreateOrderDto input);
        /// <summary>
        /// Tạo đơn hàng từ giỏ hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task CreateOrderFromCart(CreateOrderFromCartDto input);
        public Task UpdateOrderStatus(UpdateOrderStatusDto input);
    }
}
