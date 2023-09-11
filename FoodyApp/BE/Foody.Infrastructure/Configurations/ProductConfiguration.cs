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
            builder.HasOne(x => x.Promotion).WithMany(x => x.Products).HasForeignKey(x => x.PromotionId);
            builder.HasMany(c => c.ProductImages).WithOne(p => p.Product).HasForeignKey(c => c.ProductId);
        }
    }
}
