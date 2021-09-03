namespace TaxCalculatorUI.Services
{
    public class TaxCalculatorAPIClientOptions
    {
        public const string Position = "TaxCalculatorAPIClient";

        public string BaseAddress { get; set; }

        public int RequestTimeoutInSeconds { get; set; }
    }
}
