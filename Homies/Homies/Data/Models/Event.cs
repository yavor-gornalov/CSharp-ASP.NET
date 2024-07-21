using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Common.GlobalConstants;

namespace Homies.Data.Models;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(EventNameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(EventDescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    public string OrganiserId { get; set; } = null!;
    public IdentityUser Organiser { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime Start { get; set; }

    [Required]
    public DateTime End { get; set; }

    [Required]
    [ForeignKey(nameof(Type))]
    public int TypeId { get; set; }
    public Type Type { get; set; } = null!;

    public IList<EventParticipant> EventsParticipants { get; set; } = new List<EventParticipant>();
}
