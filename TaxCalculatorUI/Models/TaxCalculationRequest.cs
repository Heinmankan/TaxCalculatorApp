using System.ComponentModel.DataAnnotations;

namespace TaxCalculatorUI.Models
{
    public class TaxCalculationRequest
    {
        [Required(ErrorMessage = "Please enter a valid postal code")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Please enter a valid annual income")]
        [DataType(DataType.Currency)]
        public decimal AnnualIncome { get; set; }
    }
}
