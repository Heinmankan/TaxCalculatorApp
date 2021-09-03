using TaxCalculatorService.Models;

namespace TaxCalculatorService.Interfaces
{
    public interface ITaxCalculation
    {
        TaxCalculationResponse Calculate(decimal annualIncome);
    }
}
