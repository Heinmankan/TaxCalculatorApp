using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using System;
using System.Linq;
using TaxCalculatorDB.Models;
using TaxCalculatorService.Enums;

namespace TaxCalculatorService.Tests
{
    [TestFixture]
    public partial class TaxCalculatorTests
    {
        [Test]
        [TestCase(0, "0")]
        [TestCase(55000, "9625")]
        [TestCase(200000, "35000")]
        [TestCase(200001, "35000.175")]
        [TestCase(1400000, "245000")]
        [TestCase(1400000.01, "245000.00175")]
        public void TestFlatRateTax(decimal annualIncome, decimal expectedTax)
        {
            var postalCode = "3456";
            var taxCalculation = (int)TaxCalculationTypeEnum.FlatRate;

            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());

            context.PostalCodeTaxCalculationLinks.Add(new PostalCodeTaxCalculation
            {
                PostalCode = postalCode,
                TaxCalculationType = taxCalculation,
                IsActive = true
            });
            context.SaveChanges();

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var taxCalculationFactory = GetTaxCalculationFactory(loggerFactory, context);

            var taxCalculator = new TaxCalculator(loggerFactory, context, taxCalculationFactory);

            var taxResult = taxCalculator.Calculate(postalCode, annualIncome);

            Assert.IsNotNull(taxResult);
            Assert.AreEqual(expectedTax, taxResult.CalculationResult);

            var taxCalculationResultCount = context.TaxCalculations.Count();

            Assert.AreEqual(1, taxCalculationResultCount);

            var taxCalculationResult = context.TaxCalculations
                .FirstOrDefault(x => x.PostalCode == postalCode
                                  && x.TaxCalculationType == taxCalculation
                                  && x.CreatedAt > DateTime.Now.AddMinutes(-1));

            Assert.IsNotNull(taxCalculationResult);
            Assert.AreEqual(taxResult.CalculationResult, taxCalculationResult.Result);
        }
    }
}
