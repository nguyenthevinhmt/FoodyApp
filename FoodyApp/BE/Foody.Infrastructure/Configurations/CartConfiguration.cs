using Foody.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Infrastructure.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(x => x.Id);
            builder.HasMany(c => c.Products)
                    .WithMany(p => p.Carts)
                    .UsingEntity<ProductCart>(
                        r => r.HasOne(e => e.Product).WithMany(e => e.ProductCarts).HasForeignKey(e => e.ProductId),
                        l => l.HasOne(e => e.Cart).WithMany(e => e.ProductCarts).HasForeignKey(e => e.CartId)
            );

        }
    }
}
