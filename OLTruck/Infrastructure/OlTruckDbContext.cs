using Microsoft.EntityFrameworkCore;
using OLTruck.Domain.Models;

namespace OLTruck.Infrastructure
{
    public class OlTruckDbContext : DbContext
    {
        public OlTruckDbContext(DbContextOptions<OlTruckDbContext> options) : base(options) { }
        public DbSet<Truck> Trucks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Truck>(entity =>
            {
                entity.HasKey(t => t.Code);
                entity.Property(t => t.Name).IsRequired();
                entity.Property(t => t.Description);
                entity.Property(t => t.Status);
            });
        }
    }
}
