using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.CartServices.Interfaces;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foody.Application.Services.CartServices.Implements
{
    public class CartService : ICartService
    {
        private readonly FoodyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(FoodyAppContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
            #region quản lý đơn hàng nháp (giỏ hàng)

        //Thêm sản phẩm vào giỏ hàng
        public async Task<string> AddProductToCart(int productId)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == currentUserId);
                if (cart == null)
                {

                    cart = new Cart
                    {
                        UserId = currentUserId,
                        CreatedBy = currentUserId,
                        CreatedAt = DateTime.Now,
                        IsDeleted = false,
                    };
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();
                }

                var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(pc => pc.CartId == cart.Id && pc.ProductId == productId && pc.IsDeleted == false);
                if (productCart == null)
                {
                    // Nếu sản phẩm chưa có, thêm mới
                    productCart = new ProductCart
                    {
                        ProductId = productId,
                        Quantity = 1,
                        CartId = cart.Id,
                        IsDeleted = false,
                    };
                    await _context.ProductsCarts.AddAsync(productCart);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    // Nếu sản phẩm đã có, cập nhật số lượng
                    productCart.Quantity += 1;
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                return "Thêm sản phẩm vào giỏ hàng thành công";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return "Lỗi: " + ex.Message;
            }
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public async Task RemoveProductFromCart(int productId)
        {
            var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(od => od.ProductId == productId && od.IsDeleted == false);

            if (productCart == null)
            {
                throw new UserFriendlyException("Không tìm thấy sản phẩm trong giỏ hàng");
            }

            productCart.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
        //Lấy danh sách sản phẩm trong giỏ hàng
        public async Task<CartResponseDto> GetCartByUserId()
        {
            var userId = CommonUtils.GetUserId(_httpContextAccessor);
            var shoppingCart = await (from cart in _context.Carts
                                      join productCart in _context.ProductsCarts on cart.Id equals productCart.CartId
                                      join product in _context.Products on productCart.ProductId equals product.Id
                                      join pp in _context.ProductPromotions on product.Id equals pp.ProductId
                                      join promotion in _context.Promotions on pp.PromotionId equals promotion.Id
                                      where cart.UserId == userId
                                      && product.IsActived == true && product.IsDeleted == false
                                      && pp.IsActive == true && productCart.IsDeleted == false
                                      group new { cart, productCart, product, promotion } by cart.Id into grouped
                                      select new CartResponseDto
                                      {
                                          CartId = grouped.Key,
                                          TotalPrice = grouped.Sum(g => g.productCart.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.promotion.DiscountPercent / 100)),
                                          Products = grouped.Select(p => new InfoProductCart1Dto
                                          {
                                              ProductCartId = p.productCart.Id,
                                              Id = p.product.Id,
                                              Name = p.product.Name,
                                              ActualPrice = p.product.ActualPrice - (p.product.ActualPrice * p.promotion.DiscountPercent / 100),
                                              CategoryId = p.product.CategoryId,
                                              Description = p.product.Description,
                                              ProductImageUrl = p.product.ProductImages.Select(o => o.ProductImageUrl).FirstOrDefault(),
                                              Quantity = p.productCart.Quantity,
                                              CreateBy = p.product.CreatedBy,
                                              Price = p.product.Price,
                                              IsActive = p.product.IsActived,

                                          }).ToList(),
                                      }).FirstOrDefaultAsync();
            if (shoppingCart == null)
            {
                throw new UserFriendlyException("Chưa có sản phẩm nào trong giỏ hàng");
            }
            return shoppingCart;
        }

        public async Task<string> UpdateQuantity(UpdateCartDto input)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == currentUserId);
                if (cart == null)
                {
                    throw new UserFriendlyException("Chưa tồn tại giỏ hàng!");
                }
                else
                {
                    var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(pc => pc.CartId == cart.Id && pc.ProductId == input.productId && pc.IsDeleted == false);
                    if (productCart == null)
                    {
                        throw new UserFriendlyException("Không tìm thấy sản phẩm có trong giỏ hàng!");
                    }
                    else
                    {
                        int value = productCart.Quantity + input.quantity;
                        if (value <= 0)
                        {
                            productCart.IsDeleted = true;
                            await _context.SaveChangesAsync();
                            return "sản phẩm đã được xóa khỏi giỏ hàng";
                        }
                        else
                        {
                            productCart.Quantity = value;
                            await _context.SaveChangesAsync();
                        }
                    }
                }
                return "Cập nhật số lượng thành công.";
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        #endregion
    }
}
