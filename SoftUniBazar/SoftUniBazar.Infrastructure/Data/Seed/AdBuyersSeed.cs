using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Infrastructure.Data.Seed;

public class AdBuyersSeed : IEntityTypeConfiguration<AdBuyer>
{
    public void Configure(EntityTypeBuilder<AdBuyer> builder)
    {
        builder
            .HasData(new AdBuyer()
            {
                AdId = 1,
                BuyerId = "7a02b826-8f95-44b9-baa6-a4b9804daa3c"
            },
            new AdBuyer()
            {
                AdId = 2,
                BuyerId = "7a02b826-8f95-44b9-baa6-a4b9804daa3c"
            });
    }
}
