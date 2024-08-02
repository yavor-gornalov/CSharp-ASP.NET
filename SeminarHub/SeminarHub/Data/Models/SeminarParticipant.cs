using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data.Models;

public class SeminarParticipant
{
	[Key]
	public int SeminarId { get; set; }

	[ForeignKey(nameof(SeminarId))]
	public Seminar Seminar { get; set; } = null!;

	[Key]
	public string ParticipantId { get; set; } = null!;

	[ForeignKey(nameof(ParticipantId))]
	public IdentityUser Participant { get; set; } = null!;
}