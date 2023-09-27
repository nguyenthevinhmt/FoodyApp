using Foody.Application.Services.OrderServices.Dtos;

namespace Foody.Application.Services.OrderServices.Interfaces
{
    public interface IOrderService
    {
        //public Task<int> CreateDraftOrder(int UserId);
        public Task<string> AddProductToDraftOrder(int productId);
        public Task RemoveProductFromDraftOrder(int id);
        public Task<CartInfoResponse> GetDraftOrdersByUserId();
    }
}
