using System.ComponentModel.DataAnnotations;

using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Core.Models.Agent;

public class BecomeAgentFormModel
{
	[Required]
	[StringLength(AgentPhoneNumberMaxLength, MinimumLength = AgentPhoneNumberMinLength)]
	[Display(Name = "Phone Number")]
	[Phone]
	public string PhoneNumber { get; set; } = null!;
}
