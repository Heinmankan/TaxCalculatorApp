using System;
using TaxCalculatorDB.Models;

namespace TaxCalculatorDB.Helpers
{
    internal static partial class ModelBuilderExtensions
    {
        private static TaxBand[] GetTaxBands()
        {
            return new TaxBand[]
            {
                new TaxBand
                {
                    Id = 1,
                    FromBand = 0,
                    ToBand = 8350,
                    Rate = 0.01m,
                    CreatedAt = _createdAt
                },
                new TaxBand
                {
                    Id = 2,
                    FromBand = 8351,
                    ToBand = 33950,
                    Rate = 0.015m,
                    CreatedAt = _createdAt
                },
                new TaxBand
                {
                    Id = 3,
                    FromBand = 33951,
                    ToBand = 82250,
                    Rate = 0.025m,
                    CreatedAt = _createdAt
                },
                new TaxBand
                {
                    Id = 4,
                    FromBand = 82251,
                    ToBand = 171550,
                    Rate = 0.028m,
                    CreatedAt = _createdAt
                },
                new TaxBand
                {
                    Id = 5,
                    FromBand = 171551,
                    ToBand = 372950,
                    Rate = 0.033m,
                    CreatedAt = _createdAt
                },
                new TaxBand
                {
                    Id = 6,
                    FromBand = 372951,
                    ToBand = Int32.MaxValue,
                    Rate = 0.035m,
                    CreatedAt = _createdAt
                }
            };
        }
    }
}
