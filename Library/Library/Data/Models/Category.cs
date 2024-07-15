using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static Library.Common.GlobalConstants;

namespace Library.Data.Models;

public class Category
{
	[Comment("Category identifier")]
	[Key]
	public int Id { get; set; }

	[Comment("Name of the category")]
	[Required]
	[MaxLength(CategoryNameMaxLength)]
	public string Name { get; set; } = null!;

	public IList<Book> Books { get; set; } = new List<Book>();
}