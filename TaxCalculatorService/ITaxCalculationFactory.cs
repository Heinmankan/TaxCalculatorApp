using TaxCalculatorService.Enums;
using TaxCalculatorService.Interfaces;

namespace TaxCalculatorService
{
    public interface ITaxCalculationFactory
    {
        ITaxCalculation Create(TaxCalculationTypeEnum taxCalculationType);
    }
}