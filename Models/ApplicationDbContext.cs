using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Courses_API.Models
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Detail> Details { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configuración de la relación uno a uno
			modelBuilder.Entity<User>()
				.HasOne(u => u.Detail)
				.WithOne(d => d.User)
				.HasForeignKey<Detail>(d => d.UserFk);

			base.OnModelCreating(modelBuilder);
		}
	}
}
