using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Models;

public class IdentityUserBook
{
	[Comment("Book collector identifier")]
	[Required]
	[ForeignKey(nameof(IdentityUser))]
	public string CollectorId { get; set; } = null!;
	public IdentityUser Collector { get; set; } = null!;

	[Comment("Book identifier")]
	[Required]
	[ForeignKey(nameof(Book))]
	public int BookId { get; set; }
	public Book Book { get; set; } = null!;
}