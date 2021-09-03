using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TaxCalculatorDB.Configuration;
using TaxCalculatorDB.Helpers;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB
{
    public class TaxCalculationDbContext : DbContext
    {
        public TaxCalculationDbContext([NotNull] DbContextOptions<TaxCalculationDbContext> options)
            : base(options)
        { }

        public DbSet<PostalCodeTaxCalculation> PostalCodeTaxCalculationLinks { get; set; }

        public DbSet<TaxBand> TaxBands { get; set; }

        public DbSet<TaxCalculation> TaxCalculations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostalCodeTaxCalculationConfiguration());
            modelBuilder.ApplyConfiguration(new TaxBandConfiguration());
            modelBuilder.ApplyConfiguration(new TaxCalculationConfiguration());

            modelBuilder.Seed();
        }
    }
}
