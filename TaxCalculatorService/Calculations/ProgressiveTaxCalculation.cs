using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using TaxCalculatorDB;
using TaxCalculatorService.Interfaces;
using TaxCalculatorService.Models;

namespace TaxCalculatorService.Calculations
{
    public class ProgressiveTaxCalculation : ITaxCalculation
    {
        private readonly ILogger<ProgressiveTaxCalculation> _logger;
        private readonly TaxCalculationDbContext _taxCalculationDbContext;

        public ProgressiveTaxCalculation(ILoggerFactory loggerFactory, TaxCalculationDbContext taxCalculationDbContext)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<ProgressiveTaxCalculation>();
            _taxCalculationDbContext = taxCalculationDbContext ?? throw new ArgumentNullException(nameof(taxCalculationDbContext));
        }

        public TaxCalculationResponse Calculate(decimal annualIncome)
        {
            _logger.LogInformation("Calculate Progressive tax on {annualIncome}", annualIncome);

            var taxBands = _taxCalculationDbContext.TaxBands.OrderBy(x => x.FromBand).ToList();

            _logger.LogDebug("Tax bands:\n{taxBands}", JsonConvert.SerializeObject(taxBands, Formatting.Indented));

            var salary = annualIncome;
            decimal taxToBePaid = 0.0m;

            foreach (var band in taxBands)
            {
                if (annualIncome > band.FromBand)
                {
                    var fromBand = band.FromBand == 0 ? 0 : band.FromBand - 1;
                    var taxableAtThisRate = Math.Min(band.ToBand - fromBand, salary - fromBand);
                    var taxThisBand = taxableAtThisRate * band.Rate;
                    taxToBePaid += taxThisBand;
                }
            }

            _logger.LogDebug("Result: {taxToBePaid}", taxToBePaid);

            return new TaxCalculationResponse
            {
                CalculationResult = taxToBePaid
            };
        }
    }
}
