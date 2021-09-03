using System.Threading.Tasks;
using TaxCalculatorUI.Models;

namespace TaxCalculatorUI.Services
{
    public interface ITaxCalculatorAPIClient
    {
        Task<TaxCalculationResponse> CalculateTax(TaxCalculationRequest request);
    }
}