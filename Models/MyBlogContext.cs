using Microsoft.EntityFrameworkCore;

namespace CS58_Razor09_Entity_ASP.Models
{
	public class MyBlogContext : DbContext
	{
		public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
		public DbSet<Article> Articles { get; set; }
	}
}
