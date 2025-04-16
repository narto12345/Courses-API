using Microsoft.EntityFrameworkCore;

namespace Courses_API.Models
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Detail> Details { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Lesson> Lessons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configuración de la relación uno a uno
			modelBuilder.Entity<User>()
				.HasOne(u => u.Detail)
				.WithOne(d => d.User)
				.HasForeignKey<Detail>(d => d.UserIdFk);

			base.OnModelCreating(modelBuilder);
		}
	}
}
