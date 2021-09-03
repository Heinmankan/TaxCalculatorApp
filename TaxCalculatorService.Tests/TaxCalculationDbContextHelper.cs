using Microsoft.EntityFrameworkCore;
using System;
using TaxCalculatorDB;

namespace TaxCalculatorService.Tests
{
    public static class TaxCalculationDbContextHelper
    {
        internal static DbContextOptions<TaxCalculationDbContext> GetTaxCalculationDbContextOptions(Guid id)
        {
            return new DbContextOptionsBuilder<TaxCalculationDbContext>()
                .UseInMemoryDatabase(databaseName: $"InMemoryDatabase_{id}")
                .Options;
        }

        internal static TaxCalculationDbContext GetTaxCalculationDbContext(Guid id)
        {
            return new TaxCalculationDbContext(GetTaxCalculationDbContextOptions(id));
        }

        internal static TaxCalculationDbContext GetTaxCalculationDbContext(DbContextOptions<TaxCalculationDbContext> options)
        {
            return new TaxCalculationDbContext(options);
        }
    }
}
