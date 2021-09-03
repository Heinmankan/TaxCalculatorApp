using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Helpers
{
    internal static partial class ModelBuilderExtensions
    {
        private static PostalCodeTaxCalculation[] GetPostalCodeTaxCalculationLinks()
        {
            return new PostalCodeTaxCalculation[]
            {
                new PostalCodeTaxCalculation
                {
                    Id = 1,
                    PostalCode = "7441",
                    TaxCalculationType = 1, // Progressive Tax Calculation
                    IsActive = true,
                    CreatedAt = _createdAt
                },
                new PostalCodeTaxCalculation
                {
                    Id = 2,
                    PostalCode = "A100",
                    TaxCalculationType = 2, // Flat Value Tax Calculation
                    IsActive = true,
                    CreatedAt = _createdAt
                },
                new PostalCodeTaxCalculation
                {
                    Id = 3,
                    PostalCode = "7000",
                    TaxCalculationType = 3, // Flat Rate Tax Calculation
                    IsActive = true,
                    CreatedAt = _createdAt
                },
                new PostalCodeTaxCalculation
                {
                    Id = 4,
                    PostalCode = "1000",
                    TaxCalculationType = 1, // Progressive Tax Calculation
                    IsActive = true,
                    CreatedAt = _createdAt
                }
            };
        }
    }
}
