using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using TaxCalculatorDB;
using TaxCalculatorDB.Models;
using TaxCalculatorService.Enums;
using TaxCalculatorService.Models;

namespace TaxCalculatorService
{
    public class TaxCalculator : ITaxCalculator
    {
        private readonly ILogger<TaxCalculator> _logger;
        private readonly TaxCalculationDbContext _taxCalculationDbContext;
        private readonly ITaxCalculationFactory _taxCalculationFactory;

        public TaxCalculator(ILoggerFactory loggerFactory, TaxCalculationDbContext taxCalculationDbContext, ITaxCalculationFactory taxCalculationFactory)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<TaxCalculator>(); ;
            _taxCalculationDbContext = taxCalculationDbContext;
            _taxCalculationFactory = taxCalculationFactory ?? throw new ArgumentNullException(nameof(taxCalculationFactory));
        }

        public TaxCalculationResponse Calculate(string postalCode, decimal annualIncome)
        {
            _logger.LogInformation("Calculate tax for postal code {postalCode} and annual income {annualIncome}", postalCode, annualIncome);

            if (annualIncome < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(annualIncome), $"Invalid annual income: {annualIncome:0.00}");
            }

            var postalCodeTaxCalculation = _taxCalculationDbContext.PostalCodeTaxCalculationLinks
                .FirstOrDefault(x => x.IsActive && x.PostalCode == postalCode);

            if (postalCodeTaxCalculation == null)
            {
                throw new ArgumentOutOfRangeException(nameof(postalCode), $"Unable to map postal code: {postalCode} ");
            }

            var taxCalculationType = (TaxCalculationTypeEnum)postalCodeTaxCalculation.TaxCalculationType;

            var taxCalculation = _taxCalculationFactory.Create(taxCalculationType);

            var taxResult = taxCalculation.Calculate(annualIncome);

            _taxCalculationDbContext.TaxCalculations.Add(
                new TaxCalculation
                {
                    PostalCode = postalCode,
                    AnnualIncome = annualIncome,
                    TaxCalculationType = (int)taxCalculationType,
                    Result = taxResult.CalculationResult,
                    CreatedAt = DateTime.Now
                }
            );
            _taxCalculationDbContext.SaveChanges();

            return taxResult;
        }
    }
}
