using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculatorUI.Models;
using TaxCalculatorUI.Services;

namespace TaxCalculatorUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITaxCalculatorAPIClient _taxCalculatorAPIClient;

        public IndexModel(ILogger<IndexModel> logger, ITaxCalculatorAPIClient taxCalculatorAPIClient)
        {
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            _taxCalculatorAPIClient = taxCalculatorAPIClient ?? throw new System.ArgumentNullException(nameof(taxCalculatorAPIClient));
        }

        [BindProperty]
        public TaxCalculationRequest TaxCalculation { get; set; }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var sb = new StringBuilder();
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in allErrors)
                {
                    sb.AppendLine(error.ErrorMessage);
                }

                _logger.LogInformation("ModelState not valid:\n{modelStateError}", sb.ToString());

                return Page();
            }

            var result = await _taxCalculatorAPIClient.CalculateTax(new TaxCalculationRequest
            {
                PostalCode = TaxCalculation.PostalCode,
                AnnualIncome = TaxCalculation.AnnualIncome
            });

            return RedirectToPage("./Result", result);
        }
    }
}
