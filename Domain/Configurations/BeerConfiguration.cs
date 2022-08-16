using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Domain.Configurations
{
    public class BeerConfiguration : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.ToTable("Beer");

            builder.HasData(
                new Beer
                {
                    BeerId = 1,
                    Name = "Forte Hendrik Quadrupel",
                    AlcoholContent = 11,
                    SellingPriceToWholesalers = 3.99,
                    SellingPriceToClients = 10.99,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 2,
                    Name = "Brugse Zot Blond",
                    AlcoholContent = 6,
                    SellingPriceToWholesalers = 4.99,
                    SellingPriceToClients = 12.99,
                    BreweryId = 1
                },               
                new Beer
                {
                    BeerId = 3,
                    Name = "Sportzot",
                    AlcoholContent = 0.4,
                    SellingPriceToWholesalers = 1.59,
                    SellingPriceToClients = 6.99,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 4,
                    Name = "Bourgogne des Flandres",
                    AlcoholContent = 5,
                    SellingPriceToWholesalers = 0.29,
                    SellingPriceToClients = 2.59,
                    BreweryId = 2
                }
                );
        }
    }
}
