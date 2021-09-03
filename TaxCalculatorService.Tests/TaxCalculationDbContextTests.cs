using NUnit.Framework;
using System;
using System.Linq;
using TaxCalculatorDB.Models;

namespace TaxCalculatorService.Tests
{
    [TestFixture]
    public class TaxCalculationDbContextTests
    {
        [Test]
        public void TestTaxCalculationSave()
        {
            var options = TaxCalculationDbContextHelper.GetTaxCalculationDbContextOptions(Guid.NewGuid());

            using (var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(options))
            {
                context.TaxCalculations.Add(new TaxCalculation
                {
                    PostalCode = "A100",
                    AnnualIncome = 200001,
                    TaxCalculationType = 2, // Flat Value - TaxCalculationTypeEnum.cs
                    Result = 10000
                });

                context.SaveChanges();
            }

            using (var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(options))
            {
                var taxCalculationCount = context.TaxCalculations.Count();
                Assert.AreEqual(1, taxCalculationCount);

                var firstTaxCalculation = context.TaxCalculations.FirstOrDefault();
                Assert.AreEqual("A100", firstTaxCalculation.PostalCode);
                Assert.AreEqual(200001, firstTaxCalculation.AnnualIncome);
                Assert.AreEqual(10000, firstTaxCalculation.Result);
            }
        }

        [Test]
        public void TestTaxBandsSeed()
        {
            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());
            context.Database.EnsureCreated();

            var taxBandsCount = context.TaxBands.Count();
            Assert.AreEqual(6, taxBandsCount);

            var firstTaxBand = context.TaxBands.FirstOrDefault(x => x.Id == 1);
            Assert.AreEqual(0, firstTaxBand.FromBand);
            Assert.AreEqual(8350, firstTaxBand.ToBand);
            Assert.AreEqual(0.01m, firstTaxBand.Rate);
        }

        [Test]
        public void TestPostalCodeTaxCalculationSeed()
        {
            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());
            context.Database.EnsureCreated();

            var postalCodeTaxCalculationCount = context.PostalCodeTaxCalculationLinks.Count();
            Assert.AreEqual(4, postalCodeTaxCalculationCount);

            var firstTaxBand = context.PostalCodeTaxCalculationLinks.FirstOrDefault(x => x.PostalCode == "A100");
            const int flatValue = 2;
            Assert.AreEqual(flatValue, firstTaxBand.TaxCalculationType);
            Assert.AreEqual(true, firstTaxBand.IsActive);
        }
    }
}
