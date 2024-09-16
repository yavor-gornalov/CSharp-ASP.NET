using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Infrastructure.Data.Extensions;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Infrastructure.Data;

public class BazarDbContext : IdentityDbContext
{
    public BazarDbContext(DbContextOptions<BazarDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ad> Ads { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<AdBuyer> AdBuyers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure();
        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}