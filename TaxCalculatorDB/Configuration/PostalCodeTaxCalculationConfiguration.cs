using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Configuration
{
    public class PostalCodeTaxCalculationConfiguration : IEntityTypeConfiguration<PostalCodeTaxCalculation>
    {
        public void Configure(EntityTypeBuilder<PostalCodeTaxCalculation> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(o => o.PostalCode)
                .IsRequired();

            builder.Property(o => o.TaxCalculationType)
                .IsRequired();

            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
