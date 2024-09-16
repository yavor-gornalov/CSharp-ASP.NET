using Microsoft.EntityFrameworkCore;
using SoftUniBazar.Infrastructure.Data.Configuration;
using SoftUniBazar.Infrastructure.Data.Models;
using SoftUniBazar.Infrastructure.Data.Seed;

namespace SoftUniBazar.Infrastructure.Data.Extensions;

public static class ModelBuilderExtension
{
    public static void Configure ( this ModelBuilder builder)
    {
        builder.ApplyConfiguration<Category>(new CategoryConfiguration());
        builder.ApplyConfiguration<Ad>(new AdConfiguration());
        builder.ApplyConfiguration<AdBuyer>(new AdBuyerConfiguration());
    }
    
    public static void Seed(this ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategorySeed());
    }

}
