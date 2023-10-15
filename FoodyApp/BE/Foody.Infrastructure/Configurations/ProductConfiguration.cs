using Foody.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.HasMany(x => x.ProductPromotion).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            builder.HasMany(c => c.ProductImages).WithOne(p => p.Product).HasForeignKey(c => c.ProductId);
            builder.HasMany(c => c.Carts).WithMany(p => p.Products).UsingEntity<ProductCart>(
                        r => r.HasOne(e => e.Cart).WithMany(e => e.ProductCarts).HasForeignKey(e => e.CartId),
                        l => l.HasOne(e => e.Product).WithMany(e => e.ProductCarts).HasForeignKey(e => e.ProductId)
            );
        }
    }
}
