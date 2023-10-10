﻿using Foody.Application.Services.CartServices.Dtos;
using Foody.Application.Services.CartServices.Interfaces;
using Foody.Application.Services.OrderServices.Dtos;
using Foody.Domain.Constants;
using Foody.Domain.Entities;
using Foody.Infrastructure.Persistence;
using Foody.Share.Exceptions;
using Foody.Share.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foody.Application.Services.CartServices.Implements
{
    public class CartService : ICartService
    {
        private readonly FoodyAppContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(FoodyAppContext context, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        #region quản lý đơn hàng nháp (giỏ hàng)

        //Thêm sản phẩm vào giỏ hàng
        public async Task<string> AddProductToCart(int productId)
        {
            var currentUserId = CommonUtils.GetUserId(_httpContextAccessor);
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == currentUserId);
                    if (cart == null)
                    {
                        await _context.Carts.AddAsync(new Cart
                        {
                            UserId = currentUserId,
                            CreatedBy = currentUserId,
                            CreatedAt = DateTime.Now,
                            IsDeleted = false,
                        });
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(od => od.CartId == cart.Id && od.ProductId == productId);
                        if (productCart == null)
                        {
                            await _context.ProductsCarts.AddAsync(new ProductCart
                            {
                                ProductId = productId,
                                Quantity = 1,
                                CartId = cart.Id
                            });
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            productCart.Quantity += 1;
                            await _context.SaveChangesAsync();
                        }
                    }
                    await transaction.CommitAsync();
                    return "Thêm thành công";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return "Lỗi: " + ex.Message;
                }
            }
        }

        //Xóa sản phẩm khỏi giỏ hàng
        public async Task RemoveProductFromCart(int productId)
        {
            var productCart = await _context.ProductsCarts.FirstOrDefaultAsync(od => od.Id == productId);

            if (productCart == null)
            {
                throw new UserFriendlyException("Không tìm thấy sản phẩm trong giỏ hàng");
            }

            _context.ProductsCarts.Remove(productCart);
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
                                      && pp.IsActive == true
                                      group new { cart, productCart, product, promotion } by cart.Id into grouped
                                      select new CartResponseDto
                                      {
                                          CartId = grouped.Key,
                                          TotalPrice = grouped.Sum(g => g.productCart.Quantity * (g.product.ActualPrice - g.product.ActualPrice * g.promotion.DiscountPercent / 100)),
                                          Products = grouped.Select(p => new InfoProductCartDto
                                          {
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

        #endregion
    }
}