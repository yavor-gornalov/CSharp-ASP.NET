using Microsoft.AspNetCore.Identity;
using SeminarHub.Data.Common;
using SeminarHub.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeminarHub.Models;

public class SeminarAllViewModel
{
	public int Id { get; set; }

	public string Topic { get; set; } = null!;

	public string Lecturer { get; set; } = null!;

	public string Organizer { get; set; } = null!;

	public string DateAndTime { get; set; } = null!;

	public string Category { get; set; } = null!;

}
