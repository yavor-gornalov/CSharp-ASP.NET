using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameZone.Infrastructure.Validations.GlobalConstants;

namespace GameZone.Data.Models;

public class Game
{
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(GameDescriptionMaxLength)]
	public string Name { get; set; } = null!;

	[Required]
	[MaxLength(GameDescriptionMaxLength)]
	public string Description { get; set; } = null!;

	public string? ImageUrl { get; set; }

	[Required]
	[ForeignKey(nameof(Publisher))]
	public string PublisherId { get; set; } = null!;

	[Required]
	public IdentityUser Publisher { get; set; } = null!;

	[Required]
	public DateTime ReleasedOn { get; set; }

	[Required]
	[ForeignKey(nameof(Genre))]
	public int GenreId { get; set; }

	[Required]
	public Genre Genre { get; set; } = null!;

	public ICollection<GamerGame> GamersGames { get; set; } = new HashSet<GamerGame>();
}
