using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
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
        [TestCase(55000, "2750")]
        [TestCase(150555.25, "7527.7625")]
        [TestCase(199999, "9999.95")]
        [TestCase(200000, "10000")]
        [TestCase(200001, "10000")]
        [TestCase(255000, "10000")]
        public void TestFlatValueTax(decimal annualIncome, decimal expectedTax)
        {
            var postalCode = "2345";
            var taxCalculation = (int)TaxCalculationTypeEnum.FlatValue;

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
