using HouseRentingSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using static HouseRentingSystem.Infrastructure.Common.CustomClaims;

namespace HouseRentingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        private IdentityUserClaim<string> AgentUserClaim { get; set; } = null!;
        private IdentityUserClaim<string> GuestUserClaim { get; set; } = null!;
        private IdentityUserClaim<string> AdminUserClaim { get; set; } = null!;

        private ApplicationUser AgentUser { get; set; } = null!;
        private ApplicationUser GuestUser { get; set; } = null!;
        private ApplicationUser AdminUser { get; set; } = null!;

        private Agent Agent { get; set; } = null!;
        private Agent AdminAgent { get; set; } = null!;

        private Category CottageCategory { get; set; } = null!;
        private Category SingleCategory { get; set; } = null!;
        private Category DuplexCategory { get; set; } = null!;

        private House FirstHouse { get; set; } = null!;
        private House SecondHouse { get; set; } = null!;
        private House ThirdHouse { get; set; } = null!;


        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<House> Houses { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<House>()
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<House>()
                .HasOne(h => h.Agent)
                .WithMany(a => a.Houses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            SeedUsers();
            builder
                .Entity<ApplicationUser>()
                .HasData(
                    AgentUser,
                    GuestUser,
                    AdminUser
                );

            SeedAgents();
            builder
                .Entity<Agent>()
                .HasData(
                    Agent,
                    AdminAgent
                );

            SeedUserClaims();
            builder
                .Entity<IdentityUserClaim<string>>()
                .HasData(
                    AgentUserClaim,
                    GuestUserClaim,
                    AdminUserClaim
                );

            SeedCategories();
            builder
                .Entity<Category>()
                .HasData(CottageCategory, SingleCategory, DuplexCategory);

            SeedHouses();
            builder
                .Entity<House>()
                .HasData(FirstHouse, SecondHouse, ThirdHouse);

            base.OnModelCreating(builder);
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            AgentUser = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "agent@mail.com",
                NormalizedUserName = "agent@mail.com",
                Email = "agent@mail.com",
                NormalizedEmail = "agent@mail.com",
                FirstName = "Linda",
                LastName = "Michaels"
            };

            AgentUser.PasswordHash =
                 hasher.HashPassword(AgentUser, "agent123");

            GuestUser = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
                FirstName = "Teodor",
                LastName = "Lesly"
            };

            GuestUser.PasswordHash =
            hasher.HashPassword(GuestUser, "guest123");

            AdminUser = new ApplicationUser()
            {
                Id = "f1b0b8e3-6b7d-4b3e-8b1b-3f3e1b7e6b1e",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                FirstName = "Great",
                LastName = "Admin",
            };

            AdminUser.PasswordHash =
            hasher.HashPassword(AdminUser, "admin123");
        }

        private void SeedAgents()
        {
            Agent = new Agent()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = AgentUser.Id
            };

            AdminAgent = new Agent()
            {
                Id = 3,
                PhoneNumber = "+359888111111",
                UserId = AdminUser.Id
            };
        }

        private void SeedUserClaims()
        {
            AgentUserClaim = new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = AgentUser.Id,
                ClaimType = FullNameClaimType,
                ClaimValue = string.Join(" ", AgentUser.FirstName, AgentUser.LastName)
            };

            GuestUserClaim = new IdentityUserClaim<string>()
            {
                Id = 2,
                UserId = GuestUser.Id,
                ClaimType = FullNameClaimType,
                ClaimValue = string.Join(" ", GuestUser.FirstName, GuestUser.LastName)
            };

            AdminUserClaim = new IdentityUserClaim<string>()
            {
                Id = 3,
                UserId = AdminUser.Id,
                ClaimType = FullNameClaimType,
                ClaimValue = string.Join(" ", AdminUser.FirstName, AdminUser.LastName)
            };
        }

        private void SeedCategories()
        {
            CottageCategory = new Category()
            {
                Id = 1,
                Name = "Cottage"
            };

            SingleCategory = new Category()
            {
                Id = 2,
                Name = "Single-Family"
            };

            DuplexCategory = new Category()
            {
                Id = 3,
                Name = "Duplex"
            };
        }

        private void SeedHouses()
        {
            FirstHouse = new House()
            {
                Id = 1,
                Title = "Big House Marina",
                Address = "North London, UK (near the border)",
                Description = "A big house for your whole family. Don't miss to buy a house with three bedrooms.",
                ImageUrl = "https://www.shutterstock.com/shutterstock/photos/338950502/display_1500/stock-photo-big-luxury-modern-house-at-dusk-night-time-in-suburbs-of-vancouver-canada-338950502.jpg",
                PricePerMonth = 2100.00M,
                CategoryId = DuplexCategory.Id,
                AgentId = Agent.Id,
                RenterId = GuestUser.Id
            };

            SecondHouse = new House()
            {
                Id = 2,
                Title = "Family House Comfort",
                Address = "Near the Sea Garden in Burgas, Bulgaria",
                Description = "It has the best comfort you will ever ask for. With two bedrooms, it is great for your family.",
                ImageUrl = "https://cf.bstatic.com/xdata/images/hotel/max1024x768/179489660.jpg?k=2029f6d9589b49c95dcc9503a265e292c2cdfcb5277487a0050397c3f8dd545a&o=&hp=1",
                PricePerMonth = 1200.00M,
                CategoryId = SingleCategory.Id,
                AgentId = Agent.Id
            };

            ThirdHouse = new House()
            {
                Id = 3,
                Title = "Grand House",
                Address = "Boyana Neighbourhood, Sofia, Bulgaria",
                Description = "This luxurious house is everything you will need. It is just excellent.",
                ImageUrl = "https://i.pinimg.com/originals/a6/f5/85/a6f5850a77633c56e4e4ac4f867e3c00.jpg",
                PricePerMonth = 2000.00M,
                CategoryId = SingleCategory.Id,
                AgentId = Agent.Id
            };
        }

    }
}
