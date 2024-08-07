using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models;

public class Agent
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(AgentPhoneNumberMaxLength)]
	public string PhoneNumber { get; set; } = null!;

	[Required]
	public string UserId { get; set; } = null!;
	public IdentityUser User { get; set; } = null!;
}
