using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static Library.Common.GlobalConstants;

namespace Library.Models;

public class AddBookViewModel
{
	[Required]
	[StringLength(BookTitleMaxLength, MinimumLength = BookTitleMinLength)]
	public string Title { get; set; } = null!;

	[Required]
	[StringLength(BookAuthorMaxLength, MinimumLength = BookAuthorMinLength)]
	public string Author { get; set; } = null!;

	[Required]
	public string Url { get; set; } = null!;

	[Required]
	[StringLength(BookDescriptionMaxLength, MinimumLength = BookDescriptionMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	[Range(BookRatingLowLimit, BookRatingHighLimit)]
	public decimal Rating { get; set; }

	[Required]
	public int CategoryId { get; set; }

	public IList<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
}
