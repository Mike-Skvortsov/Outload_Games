using Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}
		public DbSet<Feed> Feeds { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<News> News { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OutloudGames;Integrated Security=True");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<News>()
				.HasOne(n => n.Feed)
				.WithMany(f => f.News)
				.HasForeignKey(n => n.FeedId);
		}
	}
}
