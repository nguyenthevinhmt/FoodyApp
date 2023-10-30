using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Share.Shared.FilterDto;

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
        public Task<List<OrderResponseDto>> GetPendingOrder();
        /// <summary>
        /// Lấy đơn hàng đã được duyệt
        /// </summary>
        /// <returns></returns>
        public Task<List<OrderResponseDto>> GetAcceptOrder();
        /// <summary>
        /// Lấy đơn hàng ở trạng thái đang giao
        /// </summary>
        /// <returns></returns>
        public Task<List<OrderResponseDto>> GetShippingOrder();
        /// <summary>
        /// Lấy đơn hàng ở trạng thái giao thành công
        /// </summary>
        /// <returns></returns>
        public Task<List<OrderResponseDto>> GetSuccessOrder();
        /// <summary>
        /// Lấy tất cả đơn đã hủy
        /// </summary>
        /// <returns></returns>
        public Task<List<OrderResponseDto>> GetCanceledOrder();
        /// <summary>
        /// Tạo mới đơn hàng trực tiếp
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> CreateOrder(CreateOrderDto input);
        /// <summary>
        /// Tạo đơn hàng từ giỏ hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<int> CreateOrderFromCart(CreateOrderFromCartDto input);
        /// <summary>
        /// xử lý khi đơn hàng thanh toán thành công
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task OrderPaidSuccessResponse(int orderId);
        /// <summary>
        /// xử lý khi đơn hàng thanh toán thất bại
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task OrderFromCartFailResponse(OrderCartFailFilterDto input);
        /// <summary>
        /// Cập nhật trạng thái đơn hàng
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task UpdateOrderStatus(UpdateOrderStatusDto input);
        /// <summary>
        /// Chi tiết đơn hàng
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<OrderResponseDto> GetOrderById(int id);
        /// <summary>
        /// Lấy tất cả đơn hàng theo trạng thái chờ duyệt role Admin
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task<PageResultDto<AdminOrderDto>> GetAllOrders(OrderFilterDto input);
        /// <summary>
        /// xóa đơn hàng tạo trực tiếp từ sản phẩm ở trạng thái khởi tạo theo id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task DeleteOrder(int orderId);
    }
}
