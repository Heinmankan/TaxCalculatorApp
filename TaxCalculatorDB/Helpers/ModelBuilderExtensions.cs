using Microsoft.EntityFrameworkCore;
using System;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Helpers
{
    internal static partial class ModelBuilderExtensions
    {
        private static readonly DateTime _createdAt = new(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc);

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCodeTaxCalculation>().HasData(GetPostalCodeTaxCalculationLinks());
            modelBuilder.Entity<TaxBand>().HasData(GetTaxBands());
        }
    }
}
