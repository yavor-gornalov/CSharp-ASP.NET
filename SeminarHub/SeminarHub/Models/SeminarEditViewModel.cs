using SeminarHub.Data.Common;
using System.ComponentModel.DataAnnotations;

using static SeminarHub.Data.Common.ValidationConstants;

namespace SeminarHub.Models;

public class SeminarEditViewModel
{
	[Required]
	public int Id { get; set; }

	[Required]
	[MaxLength(SeminarTopicMaxLength)]
	[StringLength(SeminarDetailsMaxLength, MinimumLength = SeminarDetailsMinLength)]
	public string Topic { get; set; } = null!;

	[Required]
	[StringLength(SeminarLecturerMaxLength, MinimumLength = SeminarLecturerMinLength)]
	public string Lecturer { get; set; } = null!;

	[Required]
	[StringLength(SeminarDetailsMaxLength, MinimumLength = SeminarDetailsMinLength)]
	public string Details { get; set; } = null!;

	public string? OrganizerId { get; set; }

	[Required]
	[RegularExpression(DateTimeFormatRegex, ErrorMessage = $"This field, should be in format '{DateTimeDefaultFormat}'")]
	public string DateAndTime { get; set; } = null!;

	[Required]
	public int Duration { get; set; }

	[Required]
	public int CategoryId { get; set; }

	public ICollection<SeminarCategoryViewModel> Categories { get; set; } = new HashSet<SeminarCategoryViewModel>();
}
