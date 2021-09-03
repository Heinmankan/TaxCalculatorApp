using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using TaxCalculatorService.Interfaces;
using TaxCalculatorService.Models;

namespace TaxCalculatorService.Calculations
{
    public class FlatRateTaxCalculation : ITaxCalculation
    {
        private readonly ILogger<FlatRateTaxCalculation> _logger;
        private readonly FlatRateTaxCalculationOptions _options;

        public FlatRateTaxCalculation(ILoggerFactory loggerFactory, IOptions<FlatRateTaxCalculationOptions> options)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _logger = loggerFactory.CreateLogger<FlatRateTaxCalculation>();
            _options = options.Value;
        }

        public TaxCalculationResponse Calculate(decimal annualIncome)
        {
            _logger.LogInformation("Calculate Flat rate tax on {annualIncome}", annualIncome);

            var taxToBePaid = annualIncome * _options.FlatRate;

            _logger.LogDebug("Result: {taxToBePaid}", taxToBePaid);

            return new TaxCalculationResponse
            {
                CalculationResult = taxToBePaid
            };
        }
    }
}
