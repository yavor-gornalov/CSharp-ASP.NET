using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Infrastructure.Data.Models;
using System.Globalization;

namespace SoftUniBazar.Infrastructure.Data.Seed;

public class SeedAds : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        var firstAd = new Ad
        {
            Id = 1,
            Name = "Flowers",
            Description = "I have three flowers for selling. They love sunlight and need watering three times a week.",
            Price = 15,
            CategoryId = 4,
            CreatedOn = DateTime.ParseExact("15/09/2024 19:15:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            OwnerId = "0b5ca5a6-5732-4895-a96c-cce811834780",
            ImageUrl = "https://hips.hearstapps.com/hmg-prod/images/spring-flowers-65de4a13478ee.jpg",
        };

        var secondAd = new Ad
        {
            Id = 2,
            Name = "Car",
            Description = "I have a car for selling. It is in a good condition and has a new battery.",
            Price = 12000,
            CategoryId = 2,
            CreatedOn = DateTime.ParseExact("17/09/2024 14:30:00", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture),
            OwnerId = "0b5ca5a6-5732-4895-a96c-cce811834780",
            ImageUrl = "https://listings-prod.tcimg.net/listings/146110/48/82/1G1FW6S05P4138248/BF5GFZDUSPJ4G6VPYYJCY4WJV4-og-860.jpg",
        };

        builder.HasData(firstAd, secondAd);
    }
}
