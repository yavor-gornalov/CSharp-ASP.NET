using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Infrastructure.Common.ValidationConstants.Category;

namespace SoftUniBazar.Infrastructure.Data.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    public ICollection<Ad> Ads { get; set; } = new HashSet<Ad>();
}
