using Foody.Application.Services.OrderServices.Dtos;

namespace Foody.Application.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        public Task<string> AddProductToCart(int productId);
        public Task RemoveProductFromCart(int id);
        public Task<CartResponseDto> GetCartByUserId();
        public Task UpdateOrderStatus(UpdateOrderStatusDto input);
        public Task CancelOrderStatus(int orderId);
        public Task<CartResponseDto> GetPendingOrder();
        public Task<CartResponseDto> GetShippingOrder();
        public Task<CartResponseDto> GetSuccessOrder();
        public Task<CartResponseDto> GetCanceledOrder();
    }
}
