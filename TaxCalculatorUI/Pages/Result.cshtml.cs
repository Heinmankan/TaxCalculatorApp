using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaxCalculatorUI.Models;

namespace TaxCalculatorUI.Pages
{
    public class ResultModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public TaxCalculationResponse TaxCalculationResponse { get; set; }

        public void OnGet()
        { }
    }
}
