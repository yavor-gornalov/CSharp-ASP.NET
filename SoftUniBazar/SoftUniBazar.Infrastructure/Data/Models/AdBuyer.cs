using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Infrastructure.Data.Models;

public class AdBuyer
{
    [ForeignKey(nameof(Buyer))]
    public string BuyerId { get; set; } = null!;
    public IdentityUser Buyer { get; set; } = null!;

    [ForeignKey(nameof(Ad))]
    public int AdId { get; set; }
    public Ad Ad { get; set; } = null!;
}
