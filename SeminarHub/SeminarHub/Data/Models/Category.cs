using SeminarHub.Data.Common;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Data.Models;

public class Category
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstants.CategoryNameMaxLength)]
	public string Name { get; set; } = null!;

	public ICollection<Seminar> Seminars { get; set; } = new HashSet<Seminar>();
}