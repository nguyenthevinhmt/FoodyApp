using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Foody.Infrastructure.Persistence
{
    public class FoodyAppDbContextFactory : IDesignTimeDbContextFactory<FoodyAppContext>
    {
        //public FoodyAppContext CreateDbContext(string[] args)
        //{
        //    throw new NotImplementedException();
        //}
        public FoodyAppContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("FoodyAppConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<FoodyAppContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new FoodyAppContext(optionsBuilder.Options);
        }
    }
}
