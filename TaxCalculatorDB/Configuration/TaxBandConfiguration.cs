using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Configuration
{
    public class TaxBandConfiguration : IEntityTypeConfiguration<TaxBand>
    {
        public void Configure(EntityTypeBuilder<TaxBand> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseIdentityColumn(1, 1)
                .IsRequired();

            builder.Property(o => o.FromBand)
                .IsRequired();

            builder.Property(o => o.ToBand)
                .IsRequired();

            builder.Property(o => o.Rate)
                .IsRequired()
                .HasPrecision(18, 4);

            builder.Property(o => o.CreatedAt)
                .HasDefaultValueSql("getdate()");
        }
    }
}
