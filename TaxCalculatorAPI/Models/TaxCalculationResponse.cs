namespace TaxCalculatorAPI.Models
{
    public class TaxCalculationResponse
    {
        public string PostalCode { get; set; }

        public decimal AnnualIncome { get; set; }

        public decimal Result { get; set; }
    }
}
