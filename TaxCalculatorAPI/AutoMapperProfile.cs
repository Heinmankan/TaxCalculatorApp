using AutoMapper;
using TaxCalculatorAPI.Models;
using TaxCalculatorDB.Models;

namespace TaxCalculatorAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PostalCodeTaxCalculation, PostalCodeTaxCalculationDto>();
            CreateMap<PostalCodeTaxCalculationDto, PostalCodeTaxCalculation>();

            CreateMap<PostalCodeTaxCalculation, PostalCodeTaxCalculationFullDto>();
            CreateMap<PostalCodeTaxCalculationFullDto, PostalCodeTaxCalculation>();
        }
    }
}
