using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaxCalculatorUI.Helpers;
using TaxCalculatorUI.Models;

namespace TaxCalculatorUI.Services
{
    public class TaxCalculatorAPIClient : ITaxCalculatorAPIClient
    {
        private readonly HttpClient _httpClient;
        private readonly TaxCalculatorAPIClientOptions _options;
        private readonly ILogger<TaxCalculatorAPIClient> _logger;

        public TaxCalculatorAPIClient(HttpClient httpClient, IOptions<TaxCalculatorAPIClientOptions> options, ILogger<TaxCalculatorAPIClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            ConfigureHttpClient();
        }

        private void ConfigureHttpClient()
        {
            if (string.IsNullOrWhiteSpace(_options.BaseAddress))
            {
                throw new ArgumentNullException(nameof(_options.BaseAddress));
            }

            _httpClient.BaseAddress = new Uri(_options.BaseAddress);
            _httpClient.Timeout = TimeSpan.FromSeconds(double.Parse(_options.RequestTimeoutInSeconds.ToString()));
        }

        public async Task<TaxCalculationResponse> CalculateTax(TaxCalculationRequest request)
        {
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/TaxCalculation")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(requestMessage);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var badRequestString = await response.Content.ReadAsStringAsync();

                    return await Task.Run(() => new TaxCalculationResponse
                    {
                        ErrorMessage = badRequestString
                    });
                }

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<TaxCalculationResponse>(responseString));
            }
            catch (HttpRequestException ex)
            {
                var errorMessage = ex.GetNestedMessage();

                _logger.LogError(ex, $"{errorMessage}");
                return new TaxCalculationResponse
                {
                    ErrorMessage = errorMessage
                };
            }
            catch (Exception ex)
            {
                var errorMessage = ex.GetNestedMessage();

                _logger.LogError(ex, $"Unexpected exception: {errorMessage}");
                return new TaxCalculationResponse
                {
                    ErrorMessage = errorMessage
                };
            }
        }
    }
}
