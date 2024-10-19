using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DeskMarket.CustomAttributes
{
    public class DateFormatAttribute(string dateFormat) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string errorMessage = $"Date should be in \"{dateFormat}\" format.";

            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (value is not string)
            {
                return new ValidationResult(errorMessage);
            }

            var date = (string)value;

            if (DateTime.TryParseExact(date, dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(errorMessage);
        }
    }
}
