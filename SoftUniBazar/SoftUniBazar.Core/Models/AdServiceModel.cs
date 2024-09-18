using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Infrastructure.Common.ValidationConstants.Ad;

namespace SoftUniBazar.Core.Models;

public class AdServiceModel
{
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength,
        ErrorMessage = "The {0} must be between {2} and {1} characters.")]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength,
        ErrorMessage = "The {0} must be between {2} and {1} characters.")]
    public string Description { get; set; } = null!;

    [Required]
    [Url(ErrorMessage = "The ImageUrl must be a valid URL.")]
    public string ImageUrl { get; set; } = null!;

    [Required]
    [Range(MinPrice, MaxPrice, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    [Required]
    [Display(Name = "Category")]
    public int CategoryId { get; set; }

    // This property is used to populate the dropdown for categories.
    public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
}