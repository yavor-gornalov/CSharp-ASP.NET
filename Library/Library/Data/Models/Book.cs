using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Common.GlobalConstants;

namespace Library.Data.Models;

[Comment("Library Books")]
public class Book
{
	[Comment("Book identifier")]
	[Key]
	public int Id { get; set; }

	[Comment("Title of the book")]
	[Required]
	[MaxLength(BookTitleMaxLength)]
	public string Title { get; set; } = null!;

	[Comment("Author of the book")]
	[Required]
	[MaxLength(BookAuthorMaxLength)]
	public string Author { get; set; } = null!;

	[Comment("Description of the book")]
	[Required]
	[MaxLength(BookDescriptionMaxLength)]
	public string Description { get; set; } = null!;

	[Comment("Rating of the book")]
	[Required]
	public decimal Rating { get; set; }

	[Comment("Category of the book")]
	[Required]
	[ForeignKey(nameof(Category))]
	public int CategoryId { get; set; }
	public Category Category { get; set; } = null!;

	public IList<IdentityUserBook> UsersBooks { get; set; } = new List<IdentityUserBook>();

}
