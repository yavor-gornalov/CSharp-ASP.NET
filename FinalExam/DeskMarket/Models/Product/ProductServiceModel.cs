using DeskMarket.CustomAttributes;
using DeskMarket.Models.Category;
using System.ComponentModel.DataAnnotations;

using static DeskMarket.Data.Constants.ValidationConstants;

namespace DeskMarket.Models.Product;

public class ProductServiceModel
{
    [Required]
    [StringLength(ProductNameMaxLength, MinimumLength = ProductNameMinLength)]
    public string ProductName { get; set; } = null!;

    [Required]
    [Range(typeof(decimal), ProductPriceMin, ProductPriceMax)]
    public decimal Price { get; set; }

    [Required]
    [StringLength(ProductDescriptionMaxLength, MinimumLength = ProductDescriptionMinLength)]
    public string Description { get; set; } = null!;

    [Url]
    public string? ImageUrl { get; set; }

    [Required]
    [DateFormat(DateTimeDefaultFormat)]
    public string AddedOn { get; set; } = null!;

    [Required]
    public int CategoryId { get; set; }

    public string? SellerId { get; set; } = null!;

    public IEnumerable<CategoryAllViewModel> Categories { get; set; } = new HashSet<CategoryAllViewModel>();
}
