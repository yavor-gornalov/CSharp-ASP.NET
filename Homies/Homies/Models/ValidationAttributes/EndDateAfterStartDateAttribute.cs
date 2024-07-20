using Homies.Models;
using System.ComponentModel.DataAnnotations;

public class EndDateAfterStartDateAttribute : ValidationAttribute
{
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		var model = (AddEventViewModel)validationContext.ObjectInstance;

		if (model.End < model.Start)
		{
			return new ValidationResult("End date must not be before Start date.");
		}

		return ValidationResult.Success;
	}
}