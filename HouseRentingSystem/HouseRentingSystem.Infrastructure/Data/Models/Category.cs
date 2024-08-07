using System.ComponentModel.DataAnnotations;

using static HouseRentingSystem.Infrastructure.Common.ValidationConstants;

namespace HouseRentingSystem.Infrastructure.Data.Models;

public class Category
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(CategoryNameMaxLength)]
	public string Name { get; set; } = null!;

	public ICollection<House> Houses { get; set; } = new HashSet<House>();
}
