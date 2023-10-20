using JSONAPI_WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace JSONAPI_WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductId)
                .IsUnique();

            modelBuilder.Entity<Product>()
            .Property(p => p.UpdatedAt)
            .ValueGeneratedOnUpdate();
        }
    }
}
