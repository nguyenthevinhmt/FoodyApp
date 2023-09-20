using Foody.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Infrastructure.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.ToTable(nameof(Promotion));
            builder.HasKey(p => p.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Description).HasMaxLength(250);
            builder.HasMany(c => c.ProductPromotions).WithOne(c => c.Promotion).HasForeignKey(f => f.PromotionId);

            //DataSeeding
            builder.HasData(
                    new Promotion { Id = 0001, Name = "Không giảm giá", IsActive = true, DiscountPercent = 0, Description = "Không giảm giá" },
                    new Promotion { Id = 0005, Name = "Giảm giá 5%", IsActive = true, DiscountPercent = 5, Description = "Giảm giá 5%" },
                    new Promotion { Id = 0010, Name = "Giảm giá 10%", IsActive = true, DiscountPercent = 10, Description = "Giảm giá 10%" },
                    new Promotion { Id = 0020, Name = "Giảm giá 20%", IsActive = true, DiscountPercent = 20, Description = "Giảm giá 20%" },
                    new Promotion { Id = 0025, Name = "Giảm giá 25%", IsActive = true, DiscountPercent = 25, Description = "Giảm giá 25%" },
                    new Promotion { Id = 0050, Name = "Giảm giá 50%", IsActive = true, DiscountPercent = 50, Description = "Giảm giá 50%" }
                );
        }
    }
}
