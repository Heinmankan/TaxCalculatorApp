namespace TaxCalculatorService.Calculations
{
    public class FlatValueTaxCalculationOptions
    {
        public const string Position = "FlatValueTaxCalculation";

        public decimal FlatValue { get; set; }

        public decimal FlatValuePercentage { get; set; }

        public decimal FlatValueLimit { get; set; }
    }
}
