using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoftUniBazar.Infrastructure.Data.Seed;

public class SeedUsers : IEntityTypeConfiguration<IdentityUser>
{
    public IdentityUser Guest { get; set; } = null!;

    public IdentityUser Buyer { get; set; } = null!;

    public IdentityUser Seller { get; set; } = null!;

    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        var hasher = new PasswordHasher<IdentityUser>();

        Guest = new IdentityUser()
        {
            Id = "493ba206-c3d5-4f28-8372-7824f4bbcf9e",
            UserName = "guest@softuni.com",
            NormalizedUserName = "GUEST@SOFTUNI.COM",
            Email = "guest@softuni.com",
            NormalizedEmail = "GUEST@SOFTUNI.COM",
        };

        Guest.PasswordHash =
            hasher.HashPassword(Guest, "guest123");

        Buyer = new IdentityUser()
        {

            Id = "7a02b826-8f95-44b9-baa6-a4b9804daa3c",
            UserName = "buyer@softuni.com",
            NormalizedUserName = "BUYER@SOFTUNI.COM",
            Email = "buyer@softuni.com",
            NormalizedEmail = "BUYER@SOFTUNI.COM",
        };

        Buyer.PasswordHash =
            hasher.HashPassword(Buyer, "buyer123");

        Seller = new IdentityUser()
        {

            Id = "0b5ca5a6-5732-4895-a96c-cce811834780",
            UserName = "seller@softuni.com",
            NormalizedUserName = "SELLER@SOFTUNI.COM",
            Email = "seller@softuni.com",
            NormalizedEmail = "SELLER@SOFTUNI.COM",
        };

        Seller.PasswordHash =
            hasher.HashPassword(Seller, "seller123");

        builder.HasData(Guest, Buyer, Seller);
    }
}
