using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Configurations;

namespace Repositories.DataContext
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Brewery> Brewery { get; set; }
        public DbSet<Beer> Beer { get; set; }
        public DbSet<Wholesaler> Wholesaler { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<InventoryBeer> InventoryBeer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BreweryConfiguration());
            modelBuilder.ApplyConfiguration(new BeerConfiguration());
            modelBuilder.ApplyConfiguration(new WholesalerConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryBeerConfiguration());
        }
    }
}
