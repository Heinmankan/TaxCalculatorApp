using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculatorUI.Services;

namespace TaxCalculatorUI.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TaxCalculatorAPIClientOptions>(configuration.GetSection(TaxCalculatorAPIClientOptions.Position));
            services.AddTransient<ITaxCalculatorAPIClient, TaxCalculatorAPIClient>();

            return services;
        }
    }
}
