using GameZone.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data
{
	public class GameZoneDbContext : IdentityDbContext
	{
		public GameZoneDbContext(DbContextOptions<GameZoneDbContext> options)
			: base(options)
		{
		}

		public DbSet<Game> Games { get; set; } = null!;

		public DbSet<Genre> Genres { get; set; } = null!;

		public DbSet<GamerGame> GamersGames { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<GamerGame>()
				.HasKey(gg => new { gg.GameId, gg.GamerId });

			builder.Entity<GamerGame>()
				.HasOne(g => g.Game)
				.WithMany(g => g.GamersGames)
				.HasForeignKey(g => g.GameId)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.Entity<Genre>()
				.HasData(
				new Genre { Id = 1, Name = "Action" },
				new Genre { Id = 2, Name = "Adventure" },
				new Genre { Id = 3, Name = "Fighting" },
				new Genre { Id = 4, Name = "Sports" },
				new Genre { Id = 5, Name = "Racing" },
				new Genre { Id = 6, Name = "Strategy" });

			base.OnModelCreating(builder);
		}
	}
}
