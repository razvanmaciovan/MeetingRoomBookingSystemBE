using Locus.Models;
using Microsoft.EntityFrameworkCore;
namespace Locus.Data
{
    public class EntitiesDbContext : DbContext
    {
        public EntitiesDbContext(DbContextOptions<EntitiesDbContext> options):base(options)
        {}
        public DbSet<User> Users { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Layout> Layouts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasOne(x => x.Tenant)
            //    .WithMany(x => x.Users);
            //modelBuilder.UseSerialColumns();
        }
    }
}
