using System;

namespace TaxCalculatorDB.Models
{
    public class TaxCalculation
    {
        public Guid Id { get; set; }

        public int TaxCalculationType { get; set; }

        public string PostalCode { get; set; }

        public decimal AnnualIncome { get; set; }

        public decimal Result { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
