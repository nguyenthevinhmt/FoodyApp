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
            builder.HasMany(c => c.Products).WithOne(p => p.Categories).HasForeignKey(c => c.CategoryId);
            //Data seeding
            builder.HasData(
                    new Category { Id = 0001, Name = "Cơm", Description = "Các món cơm", CreatedAt = DateTime.Now, IsDeleted = false },
                    new Category { Id = 0002, Name = "Đồ ăn nhanh", Description = "Các món ăn nhanh", CreatedAt = DateTime.Now, IsDeleted = false },
                    new Category { Id = 0003, Name = "Đồ uống", Description = "Các đồ uống", CreatedAt = DateTime.Now, IsDeleted = false },
                    new Category { Id = 0004, Name = "Bún", Description = "Các món bún", CreatedAt = DateTime.Now, IsDeleted = false },
                    new Category { Id = 0005, Name = "Mì", Description = "Các món mì", CreatedAt = DateTime.Now, IsDeleted = false }
                );
        }
    }
}
