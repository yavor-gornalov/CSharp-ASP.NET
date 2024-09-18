using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace SoftUniBazar.CustomModelBinder;

public class DecimalModelBinder : IModelBinder
{

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueResult.FirstValue))
        {

            decimal result = 0;
            bool success = false;

            try
            {
                var currentDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                string stringValue = valueResult.FirstValue.Trim();
                stringValue = valueResult.FirstValue.Replace(",", currentDecimalSeparator);
                stringValue = valueResult.FirstValue.Replace(".", currentDecimalSeparator);

                result = decimal.Parse(stringValue, CultureInfo.CurrentCulture);
                success = true;
            }
            catch (FormatException fe)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
                return Task.CompletedTask;
            }
            if (success)
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
        }
        return Task.CompletedTask;
    }
}
