using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Linq;
using TaxCalculatorDB.Models;
using TaxCalculatorService.Calculations;
using TaxCalculatorService.Enums;

namespace TaxCalculatorService.Tests
{
    [TestFixture]
    public partial class TaxCalculatorTests
    {
        [Test]
        [TestCase(0, "0")]
        [TestCase(8350, "83.5")]
        [TestCase(12500, "145.75")]
        [TestCase(33950, "467.5")]
        [TestCase(67850, "1315")]
        [TestCase(82250, "1675")]
        [TestCase(155200, "3717.6")]
        [TestCase(171550, "4175.4")]
        [TestCase(298100, "8351.55")]
        [TestCase(372950, "10821.6")]
        [TestCase(400000, "11768.35")]
        [TestCase(1450000, "48518.35")]
        public void TestProgressiveTax(decimal annualIncome, decimal expectedTax)
        {
            var testPostalCode = "1234";
            var taxCalculationType = (int)TaxCalculationTypeEnum.Progressive;

            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());
            context.Database.EnsureCreated();

            context.PostalCodeTaxCalculationLinks.Add(new PostalCodeTaxCalculation
            {
                PostalCode = testPostalCode,
                TaxCalculationType = taxCalculationType,
                IsActive = true
            });
            context.SaveChanges();

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var taxCalculationFactory = GetTaxCalculationFactory(loggerFactory, context);

            var taxCalculator = new TaxCalculator(loggerFactory, context, taxCalculationFactory);

            var taxResult = taxCalculator.Calculate(testPostalCode, annualIncome);

            Assert.IsNotNull(taxResult);
            Assert.AreEqual(expectedTax, taxResult.CalculationResult);

            var taxCalculationResultCount = context.TaxCalculations.Count();

            Assert.AreEqual(1, taxCalculationResultCount);

            var taxCalculationResult = context.TaxCalculations
                .FirstOrDefault(x => x.PostalCode == testPostalCode
                                  && x.TaxCalculationType == taxCalculationType
                                  && x.CreatedAt > DateTime.Now.AddMinutes(-1));

            Assert.IsNotNull(taxCalculationResult);
            Assert.AreEqual(taxResult.CalculationResult, taxCalculationResult.Result);
        }
    }
}
