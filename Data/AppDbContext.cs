using InvWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InvWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Materiel> Materiels { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<LogList> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make username unique
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            // Make categorie name unique
            modelBuilder.Entity<Categorie>().HasIndex(c => c.CategorieName).IsUnique();
            // Make Service name unique
            modelBuilder.Entity<Service>().HasIndex(s => s.ServiceName).IsUnique();
            // Make Serial number  unique
            modelBuilder.Entity<Materiel>().HasIndex(c => c.SerialNumber).IsUnique();

        }
    }
}