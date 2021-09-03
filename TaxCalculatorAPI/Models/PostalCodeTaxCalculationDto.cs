namespace TaxCalculatorAPI.Models
{
    public class PostalCodeTaxCalculationDto
    {
        public string PostalCode { get; set; }

        public int TaxCalculationType { get; set; }

        public bool IsActive { get; set; }
    }
}
