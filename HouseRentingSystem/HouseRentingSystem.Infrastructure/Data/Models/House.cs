using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models;

public class House
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(HouseTitleMaxLength)]
	public string Title { get; set; } = null!;

	[Required]
	[MaxLength(HouseAddressMaxLength)]
	public string Address { get; set; } = null!;

	[Required]
	[MaxLength(HouseDescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[Required]
	public string ImageUrl { get; set; } = null!;

	[Required]
	[Column(TypeName = "decimal(8,2)")]
	public decimal PricePerMonth { get; set; }

	[Required]
	public int CategoryId { get; set; }
	public Category Category { get; set; } = null!;

	[Required]
	public int AgentId { get; set; }
	public Agent Agent { get; set; } = null!;

	public string? RenterId { get; set; }
}
