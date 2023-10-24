using Foody.Domain.Entities;
using Foody.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Foody.Infrastructure.Persistence
{
    public class FoodyAppContext : DbContext
    {
        public FoodyAppContext(DbContextOptions<FoodyAppContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new PromotionConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<ProductCart> ProductsCarts { get; set; }
    }
}
