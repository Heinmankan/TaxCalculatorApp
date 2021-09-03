using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TaxCalculatorService;
using TaxCalculatorService.Calculations;

namespace TaxCalculatorAPI.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.Configure<FlatRateTaxCalculationOptions>(configuration.GetSection(FlatRateTaxCalculationOptions.Position));
            services.Configure<FlatValueTaxCalculationOptions>(configuration.GetSection(FlatValueTaxCalculationOptions.Position));

            services.AddTransient<ITaxCalculationFactory, TaxCalculationFactory>();
            services.AddTransient<ITaxCalculator, TaxCalculator>();

            return services;
        }
    }
}
