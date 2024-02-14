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
        public DbSet<Log> Logs { get; set; }
    }
}