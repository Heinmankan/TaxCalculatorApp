using System;

namespace TaxCalculatorDB.Models
{
    public class TaxBand
    {
        public long Id { get; set; }

        public int FromBand { get; set; }

        public int ToBand { get; set; }

        public decimal Rate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
