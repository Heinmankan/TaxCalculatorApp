using System;

namespace TaxCalculatorDB.Models
{
    public class PostalCodeTaxCalculation
    {
        public long Id { get; set; }

        public string PostalCode { get; set; }

        public int TaxCalculationType { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
