using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using TaxCalculatorDB;
using TaxCalculatorService.Calculations;
using TaxCalculatorService.Enums;
using TaxCalculatorService.Interfaces;

namespace TaxCalculatorService
{
    public class TaxCalculationFactory : ITaxCalculationFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly TaxCalculationDbContext _taxCalculationDbContext;
        private readonly IOptions<FlatValueTaxCalculationOptions> _flatValueOptions;
        private readonly IOptions<FlatRateTaxCalculationOptions> _flatRateOptions;

        public TaxCalculationFactory(
            ILoggerFactory loggerFactory,
            TaxCalculationDbContext taxCalculationDbContext,
            IOptions<FlatValueTaxCalculationOptions> flatValueOptions,
            IOptions<FlatRateTaxCalculationOptions> flatRateOptions)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _taxCalculationDbContext = taxCalculationDbContext ?? throw new ArgumentNullException(nameof(taxCalculationDbContext));
            _flatValueOptions = flatValueOptions ?? throw new ArgumentNullException(nameof(flatValueOptions));
            _flatRateOptions = flatRateOptions ?? throw new ArgumentNullException(nameof(flatRateOptions));
        }

        public ITaxCalculation Create(TaxCalculationTypeEnum taxCalculationType)
        {
            return taxCalculationType switch
            {
                TaxCalculationTypeEnum.Progressive => new ProgressiveTaxCalculation(_loggerFactory, _taxCalculationDbContext),
                TaxCalculationTypeEnum.FlatValue => new FlatValueTaxCalculation(_loggerFactory, _flatValueOptions),
                TaxCalculationTypeEnum.FlatRate => new FlatRateTaxCalculation(_loggerFactory, _flatRateOptions),

                _ => throw new ArgumentOutOfRangeException($"Unknown {nameof(taxCalculationType)}: {taxCalculationType}"),
            };
        }
    }
}
