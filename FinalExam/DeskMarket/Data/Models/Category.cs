namespace DeskMarket.Data.Models;

using DeskMarket.Data.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static DeskMarket.Data.Constants.ValidationConstants;

[Comment("Category table")]
public class Category
{
    [Key]
    [Comment("Category identifier")]
    public int Id { get; set; }

    [Required]
    [MaxLength(CategoryNameMaxLength)]
    [Comment("Category name")]
    public string Name { get; set; } = null!;

    public IEnumerable<Product> Products { get; set; } = new HashSet<Product>();
}

