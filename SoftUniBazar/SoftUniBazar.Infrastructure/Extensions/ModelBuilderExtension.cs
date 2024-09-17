using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Infrastructure.Data.Configuration;
using SoftUniBazar.Infrastructure.Data.Models;
using SoftUniBazar.Infrastructure.Data.Seed;

namespace SoftUniBazar.Infrastructure.Extensions;

public static class ModelBuilderExtension
{
    public static void Configure(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new AdConfiguration());
        builder.ApplyConfiguration(new AdBuyerConfiguration());
    }

    public static void Seed(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SeedCategories());
        builder.ApplyConfiguration(new SeedUsers());
        builder.ApplyConfiguration(new SeedAds());
        builder.ApplyConfiguration(new AdBuyersSeed());
    }

}
