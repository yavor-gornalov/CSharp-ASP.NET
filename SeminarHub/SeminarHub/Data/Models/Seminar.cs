using Microsoft.AspNetCore.Identity;
using SeminarHub.Data.Common;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.Common.ValidationConstants;

namespace SeminarHub.Data.Models;

public class Seminar
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstants.SeminarTopicMaxLength)]
	public string Topic { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstants.SeminarLecturerMaxLength)]
	public string Lecturer { get; set; } = null!;

	[Required]
	[MaxLength(ValidationConstants.SeminarDetailsMaxLength)]
	public string Details { get; set; } = null!;

	[Required]
	public string OrganizerId { get; set; } = null!;

	[ForeignKey(nameof(OrganizerId))]
	public IdentityUser Organizer { get; set; } = null!;

	[Required]
	public DateTime DateAndTime { get; set; }

	[Required]
	public int Duration { get; set; }

	[Required]
	public int CategoryId { get; set; }

	[ForeignKey(nameof(CategoryId))]
	public Category Category { get; set; } = null!;

	public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new HashSet<SeminarParticipant>();
}