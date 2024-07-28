using System.ComponentModel.DataAnnotations;
using static GameZone.Infrastructure.Validations.GlobalConstants;

namespace GameZone.Core.Models;

public class EditGameViewModel
{
	[Required]
	public int Id { get; set; }

	[Required]
	[StringLength(GameTitleMaxLength, MinimumLength = GameTitleMinLength)]
	public string Title { get; set; } = null!;

	public string? ImageUrl { get; set; } = null!;

	[Required]
	[StringLength(GameDescriptionMaxLength, MinimumLength = GameDescriptionMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	[RegularExpression(DateTimeRegex, ErrorMessage = $"The given date format is not supported. The {nameof(ReleasedOn)} should be in format {DateTimeDefaultFormat}")]
	public string ReleasedOn { get; set; } = null!;

	[Required]
	public string PublisherId { get; set; } = null!;

	[Required]
	public int GenreId { get; set; }

	public ICollection<GenreViewModel> Genres { get; set; } = new HashSet<GenreViewModel>();
}
