using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using wbms_api.Models;

namespace wbms_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Consumer> Consumers => Set<Consumer>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Consumer>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Consumers)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
        }
    }
}
