﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Repositories.Configurations
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
                    SellingPriceToWholesalers = 3.99m,
                    SellingPriceToClients = 10.99m,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 2,
                    Name = "Brugse Zot Blond",
                    AlcoholContent = 6,
                    SellingPriceToWholesalers = 4.99m,
                    SellingPriceToClients = 12.99m,
                    BreweryId = 1
                },               
                new Beer
                {
                    BeerId = 3,
                    Name = "Sportzot",
                    AlcoholContent = 0.4,
                    SellingPriceToWholesalers = 1.59m,
                    SellingPriceToClients = 6.99m,
                    BreweryId = 1
                },
                new Beer
                {
                    BeerId = 4,
                    Name = "Bourgogne des Flandres",
                    AlcoholContent = 5,
                    SellingPriceToWholesalers = 0.29m,
                    SellingPriceToClients = 2.59m,
                    BreweryId = 2
                },
                new Beer()
                {
                    BeerId = 5,
                    Name = "Brugse Zot Dubbel",
                    AlcoholContent = 7.5,
                    SellingPriceToWholesalers = 3.99m,
                    SellingPriceToClients = 9.99m,
                    BreweryId = 1,
                    InProduction = false,
                    OutOfProductionDate = new DateTime(2022, 05, 09, 9, 15, 0)
                }
                );
        }
    }
}
