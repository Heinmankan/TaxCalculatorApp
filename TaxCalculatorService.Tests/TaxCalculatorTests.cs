using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using TaxCalculatorDB;
using TaxCalculatorDB.Models;
using TaxCalculatorService.Calculations;
using TaxCalculatorService.Enums;

namespace TaxCalculatorService.Tests
{
    [TestFixture]
    public partial class TaxCalculatorTests
    {
        ServiceProvider _provider;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            var services = new ServiceCollection();

            services.AddTransient(provider => Options.Create(new FlatRateTaxCalculationOptions
            {
                FlatRate = 0.175m
            }));

            services.AddTransient(provider => Options.Create(new FlatValueTaxCalculationOptions
            {
                FlatValue = 10000m,
                FlatValuePercentage = 0.05m,
                FlatValueLimit = 200000.0m
            }));

            _provider = services.BuildServiceProvider();
        }

        public TaxCalculationFactory GetTaxCalculationFactory(ILoggerFactory loggerFactory, TaxCalculationDbContext context)
        {
            return new TaxCalculationFactory(
                loggerFactory,
                context,
                _provider.GetService<IOptions<FlatValueTaxCalculationOptions>>(),
                _provider.GetService<IOptions<FlatRateTaxCalculationOptions>>());
        }


        [Test]
        [TestCase((int)TaxCalculationTypeEnum.Progressive)]
        [TestCase((int)TaxCalculationTypeEnum.FlatValue)]
        [TestCase((int)TaxCalculationTypeEnum.FlatRate)]
        public void TestTaxCalculationUnknownPostalCode(int taxCalculationType)
        {
            var unknownPostalCode = "ABC123";
            var knownPostalCode = "12345";
            var annualIncome = 400000;

            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());

            context.PostalCodeTaxCalculationLinks.Add(new PostalCodeTaxCalculation
            {
                PostalCode = knownPostalCode,
                TaxCalculationType = taxCalculationType,
                IsActive = true
            });
            context.SaveChanges();

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var taxCalculationFactory = GetTaxCalculationFactory(loggerFactory, context);

            var taxCalculator = new TaxCalculator(loggerFactory, context, taxCalculationFactory);

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var taxResult = taxCalculator.Calculate(unknownPostalCode, annualIncome);
            });

            Assert.NotNull(ex?.ParamName);
            Assert.NotNull(ex?.Message);
            StringAssert.AreEqualIgnoringCase("postalCode", ex.ParamName);
            StringAssert.StartsWith("Unable to map postal code: ", ex.Message);
            StringAssert.AreEqualIgnoringCase($"Unable to map postal code: {unknownPostalCode}  (Parameter 'postalCode')", ex.Message);
        }

        [Test]
        [TestCase((int)TaxCalculationTypeEnum.Progressive)]
        [TestCase((int)TaxCalculationTypeEnum.FlatValue)]
        [TestCase((int)TaxCalculationTypeEnum.FlatRate)]
        public void TestTaxCalculationInActivePostalCode(int taxCalculationType)
        {
            var knownPostalCode = "12345";
            var annualIncome = 400000;

            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());

            context.PostalCodeTaxCalculationLinks.Add(new PostalCodeTaxCalculation
            {
                PostalCode = knownPostalCode,
                TaxCalculationType = taxCalculationType,
                IsActive = false
            });
            context.SaveChanges();

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var taxCalculationFactory = new TaxCalculationFactory(
                loggerFactory,
                context,
                _provider.GetService<IOptions<FlatValueTaxCalculationOptions>>(),
                _provider.GetService<IOptions<FlatRateTaxCalculationOptions>>());

            var taxCalculator = new TaxCalculator(loggerFactory, context, taxCalculationFactory);

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var taxResult = taxCalculator.Calculate(knownPostalCode, annualIncome);
            });

            Assert.NotNull(ex?.ParamName);
            Assert.NotNull(ex?.Message);
            StringAssert.AreEqualIgnoringCase("postalCode", ex.ParamName);
            StringAssert.StartsWith("Unable to map postal code: ", ex.Message);
            StringAssert.AreEqualIgnoringCase($"Unable to map postal code: {knownPostalCode}  (Parameter 'postalCode')", ex.Message);
        }

        [Test]
        [TestCase((int)TaxCalculationTypeEnum.Progressive)]
        [TestCase((int)TaxCalculationTypeEnum.FlatValue)]
        [TestCase((int)TaxCalculationTypeEnum.FlatRate)]
        public void TestTaxCalculationNegativeAnnualIncomeError(int taxCalculationType)
        {
            var postalCode = "12345";
            var annualIncome = -400000;

            using var context = TaxCalculationDbContextHelper.GetTaxCalculationDbContext(Guid.NewGuid());

            context.PostalCodeTaxCalculationLinks.Add(new PostalCodeTaxCalculation
            {
                PostalCode = postalCode,
                TaxCalculationType = taxCalculationType,
                IsActive = true
            });
            context.SaveChanges();

            ILoggerFactory loggerFactory = new NullLoggerFactory();

            var taxCalculationFactory = new TaxCalculationFactory(
                loggerFactory,
                context,
                _provider.GetService<IOptions<FlatValueTaxCalculationOptions>>(),
                _provider.GetService<IOptions<FlatRateTaxCalculationOptions>>());

            var taxCalculator = new TaxCalculator(loggerFactory, context, taxCalculationFactory);

            var ex = Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var taxResult = taxCalculator.Calculate(postalCode, annualIncome);
            });

            Assert.NotNull(ex?.ParamName);
            StringAssert.AreEqualIgnoringCase("annualIncome", ex.ParamName);
        }
    }
}
