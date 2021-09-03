using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TaxCalculatorDB;

namespace KingPrice.Loom.Database
{
    public class TaxCalculationDbContextFactory : IDesignTimeDbContextFactory<TaxCalculationDbContext>
    {
        public TaxCalculationDbContextFactory()
        { }

        public TaxCalculationDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TaxCalculationDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new TaxCalculationDbContext(optionsBuilder.Options);
        }
    }
}
