using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoftUniBazar.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SoftUniBazar.Infrastructure.Data.Configuration;

public class AdBuyerConfiguration : IEntityTypeConfiguration<AdBuyer>
{
    public void Configure(EntityTypeBuilder<AdBuyer> builder)
    {
        builder
            .HasKey(ab => new { ab.BuyerId, ab.AdId });

        builder
            .HasOne(ab => ab.Ad)
            .WithMany(a => a.AdBuyers)
            .HasForeignKey(ab => ab.AdId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
