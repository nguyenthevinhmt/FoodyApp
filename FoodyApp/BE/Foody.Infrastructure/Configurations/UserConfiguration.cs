using Foody.Domain.Entities;
using Foody.Share.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Foody.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(250);
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(250);
            builder.Property(x => x.FirstName).HasMaxLength(250);
            builder.Property(x => x.LastName).HasMaxLength(250);

            //DataSeeding

            builder.HasData(
                    new User { Id = 1, Email = "Admin@gmail.com", Password = PasswordHasher.HashPassword("Admin@12345"), UserType = 1 },
                    new User { Id = 2, Email = "Customer@gmail.com", Password = PasswordHasher.HashPassword("Customer@12345"), UserType = 2 }
                );
        }
    }
}
