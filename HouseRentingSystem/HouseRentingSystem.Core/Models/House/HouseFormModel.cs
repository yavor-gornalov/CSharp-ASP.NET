using System.ComponentModel.DataAnnotations;
using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Core.Models.House;

public class HouseFormModel
{
	[Required]
	[StringLength(HouseTitleMaxLength, MinimumLength = HouseTitleMinLength)]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(HouseAddressMaxLength, MinimumLength = HouseAddressMinLength)]
	public string Address { get; set; } = null!;

	[Required]
	[StringLength(HouseDescriptionMaxLength, MinimumLength = HouseAddressMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	[Display(Name = "Image URL")]
	public string ImageUrl { get; set; } = null!;

	[Required]
	[Range(typeof(decimal), HousePricePerMonthMinValue, HousePricePerMonthMaxValue,
		ErrorMessage = "Price Per Month must be a positive number and less than {2} leva.")]
	[Display(Name = "Price Per Month")]
	public decimal PricePerMonth { get; set; }

	[Display(Name = "Category")]
	public int CategoryId { get; set; }

	public IEnumerable<HouseCategoryServiceModel> Categories { get; set; } = new List<HouseCategoryServiceModel>();
}
