using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaxCalculatorAPI.Models;
using TaxCalculatorService;

namespace TaxCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculationController : ControllerBase
    {
        private readonly ILogger<TaxCalculationController> _logger;
        private readonly ITaxCalculator _taxCalculator;

        public TaxCalculationController(ILoggerFactory loggerFactory, ITaxCalculator taxCalculator)
        {
            if (loggerFactory is null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<TaxCalculationController>();
            _taxCalculator = taxCalculator ?? throw new ArgumentNullException(nameof(taxCalculator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TaxCalculationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(TaxCalculationRequest request)
        {
            _logger.LogInformation("Calculate tax for postalcode {postalCode} and annual income {annualIncome}", request.PostalCode, request.AnnualIncome);

            try
            {
                var result = await Task.Run(() => _taxCalculator.Calculate(request.PostalCode, request.AnnualIncome));

                return Ok(new TaxCalculationResponse
                {
                    PostalCode = request.PostalCode,
                    AnnualIncome = request.AnnualIncome,
                    Result = result.CalculationResult
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                _logger.LogWarning(ex, $"ArgumentOutOfRangeException: {ex.GetType()} - {ex.Message}");

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected exception: {ex.GetType()} - {ex.Message}");

                throw;
            }
        }
    }
}
