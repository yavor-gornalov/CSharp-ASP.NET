namespace DeskMarket.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static DeskMarket.Data.Constants.ValidationConstants;

[Comment("Product table")]
public class Product
{
    [Key]
    [Comment("Product identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(ProductNameMaxLength)]
    [Comment("Product name")]
    public string ProductName { get; set; } = null!;

    [Required]
    [MaxLength(ProductDescriptionMaxLength)]
    [Comment("Product description")]
    public string Description { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Comment("Product price")]
    public decimal Price { get; set; }

    [MaxLength(ProductImageUrlMaxLength)]
    [Comment("Product image URL")]
    public string? ImageUrl { get; set; }

    [Required]
    [Comment("Seller identifier")]
    public string SellerId { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(SellerId))]
    public IdentityUser Seller { get; set; } = null!;

    [Required]
    [Comment("Product added on")]
    public DateTime AddedOn { get; set; }

    [Required]
    [Comment("Category identifier")]
    public int CategoryId { get; set; }

    [Required]
    [ForeignKey(nameof(CategoryId))]
    public Category Category { get; set; } = null!;

    [Required]
    [Comment("Is product deleted")]
    public bool IsDeleted { get; set; } = false;

    public IEnumerable<ProductClient> ProductsClients { get; set; } = new HashSet<ProductClient>();
}

