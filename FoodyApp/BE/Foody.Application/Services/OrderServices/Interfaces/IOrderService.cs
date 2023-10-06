using Foody.Application.Services.OrderServices.Dtos;

namespace Foody.Application.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        public Task<string> AddProductToCart(int productId);
        public Task RemoveProductFromCart(int id);
        //public Task<CartResponse> GetCartByUserId();
        public Task<CartResponse> GetCartByUserId();
        public Task UpdateOrderStatus(int orderId);
        public Task CancelOrderStatus(int orderId);
        public Task<OrderResponseDto> GetOrderByUserId(int userId);
    }
}
