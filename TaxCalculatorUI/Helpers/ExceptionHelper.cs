using System;

namespace TaxCalculatorUI.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetNestedMessage(this Exception ex)
        {
            if (ex.InnerException != null)
            {
                return GetNestedMessage(ex.InnerException);
            }

            return ex.Message;
        }
    }
}
