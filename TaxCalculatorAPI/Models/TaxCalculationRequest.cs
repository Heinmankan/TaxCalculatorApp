namespace TaxCalculatorAPI.Models
{
    public class TaxCalculationRequest
    {
        public string PostalCode { get; set; }

        public decimal AnnualIncome { get; set; }
    }
}
