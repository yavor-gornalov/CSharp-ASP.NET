using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DeskMarket.Data.Models;

[PrimaryKey(nameof(ProductId), nameof(ClientId))]
[Comment("Product client relational table")]
public class ProductClient
{
    [Required]
    [Comment("Product identifier")]
    public int ProductId { get; set; }

    [Required]
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    [Required]
    [Comment("Client identifier")]
    public string ClientId { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(ClientId))]
    public IdentityUser Client { get; set; } = null!;
}
