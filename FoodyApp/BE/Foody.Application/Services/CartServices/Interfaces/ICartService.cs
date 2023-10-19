using Foody.Application.Services.CartServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CartServices.Interfaces
{
    public interface ICartService
    {
        public Task<string> AddProductToCart(int productId);
        public Task RemoveProductFromCart(int id);
        public Task<CartResponseDto> GetCartByUserId();
        public Task<string> UpdateQuantity(UpdateCartDto input);    //quantity sẽ nhận sẽ trị dương nếu cộng thêm và âm nếu trừ đi
    }
}
