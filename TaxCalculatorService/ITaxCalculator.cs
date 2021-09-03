using TaxCalculatorService.Models;

namespace TaxCalculatorService
{
    public interface ITaxCalculator
    {
        TaxCalculationResponse Calculate(string postalCode, decimal annualIncome);
    }
}