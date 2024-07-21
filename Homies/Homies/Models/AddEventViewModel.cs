using System.ComponentModel.DataAnnotations;
using static Homies.Common.GlobalConstants;

namespace Homies.Models;

public class AddEventViewModel
{
	[Required]
	[StringLength(EventDescriptionMaxLength, MinimumLength = EventNameMinLength)]
	public string Name { get; set; } = null!;

	[Required]
	[StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength)]
	public string Description { get; set; } = null!;

	[Required]
	public DateTime Start { get; set; }

	[Required]
	[EndDateAfterStartDate]
	public DateTime End { get; set; }

	[Required]
	public int TypeId { get; set; }

	public ICollection<EventTypeViewModel> Types { get; set; } = new HashSet<EventTypeViewModel>();
}
