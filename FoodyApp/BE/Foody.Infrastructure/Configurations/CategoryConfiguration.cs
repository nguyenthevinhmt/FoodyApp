using Foody.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(250);
            builder.Property(c => c.Description).HasMaxLength(250);
            builder.HasOne(c => c.Products).WithMany(p => p.Categories).HasForeignKey(c => c.ProductId);
            builder.HasMany(c => c.CategoryImage).WithOne(p => p.Category).HasForeignKey(c => c.CategoryId);
        }
    }
}
