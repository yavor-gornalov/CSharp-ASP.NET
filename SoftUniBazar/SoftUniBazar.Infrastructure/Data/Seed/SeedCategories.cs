using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Infrastructure.Data.Models;

namespace SoftUniBazar.Infrastructure.Data.Seed;

public class SeedCategories : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .HasData(new Category()
            {
                Id = 1,
                Name = "Books"
            },
            new Category()
            {
                Id = 2,
                Name = "Cars"
            },
            new Category()
            {
                Id = 3,
                Name = "Clothes"
            },
            new Category()
            {
                Id = 4,
                Name = "Home"
            },
            new Category()
            {
                Id = 5,
                Name = "Technology"
            });
    }
}
