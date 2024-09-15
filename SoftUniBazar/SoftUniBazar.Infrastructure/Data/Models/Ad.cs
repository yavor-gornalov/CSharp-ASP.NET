using Microsoft.AspNetCore.Identity;
using SoftUniBazar.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SoftUniBazar.Infrastructure.Common.ValidationConstants.Ad;

namespace SoftUniBazar.Infrastructure.Data.Models;

public class Ad
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public string OwnerId { get; set; } = null!;

    [ForeignKey(nameof(OwnerId))]
    public IdentityUser Owner { get; set; } = null!;

    [Required]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;
    public ICollection<AdBuyer> AdBuyers { get; set; } = new HashSet<AdBuyer>();
}
