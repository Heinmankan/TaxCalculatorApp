using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using TaxCalculatorService.Interfaces;
using TaxCalculatorService.Models;

namespace TaxCalculatorService.Calculations
{
    public class FlatValueTaxCalculation : ITaxCalculation
    {
        private readonly ILogger<FlatValueTaxCalculation> _logger;
        private readonly FlatValueTaxCalculationOptions _options;

        public FlatValueTaxCalculation(ILoggerFactory loggerFactory, IOptions<FlatValueTaxCalculationOptions> options)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _logger = loggerFactory.CreateLogger<FlatValueTaxCalculation>();
            _options = options.Value;
        }

        public TaxCalculationResponse Calculate(decimal annualIncome)
        {
            _logger.LogInformation("Calculate Flat value tax on {annualIncome}", annualIncome);

            if (annualIncome < _options.FlatValueLimit)
            {
                var taxToBePaid = annualIncome * _options.FlatValuePercentage;

                _logger.LogDebug("Result: {taxToBePaid}", taxToBePaid);

                return new TaxCalculationResponse
                {
                    CalculationResult = taxToBePaid
                };
            }

            var flatValue = _options.FlatValue;

            _logger.LogDebug("Result: {taxToBePaid}", flatValue);

            return new TaxCalculationResponse
            {
                CalculationResult = flatValue
            };
        }
    }
}
