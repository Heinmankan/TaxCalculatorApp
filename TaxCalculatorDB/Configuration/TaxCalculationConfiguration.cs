using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Configuration
{
    public class TaxCalculationConfiguration : IEntityTypeConfiguration<TaxCalculation>
    {
        public void Configure(EntityTypeBuilder<TaxCalculation> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired();

            builder.Property(o => o.TaxCalculationType)
                .IsRequired();

            builder.Property(o => o.Id)
                .IsRequired();

            builder.Property(o => o.PostalCode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.AnnualIncome)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(o => o.Result)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
