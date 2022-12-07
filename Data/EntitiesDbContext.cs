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
        public DbSet<Image> Images { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>()
            //    .HasOne(x => x.Tenant)
            //    .WithMany(e => e.Users)
            //    .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.UseSerialColumns();
        }
    }
}
