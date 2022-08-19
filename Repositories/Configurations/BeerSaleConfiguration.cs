using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Repositories.Configurations
{
    public class BeerSaleConfiguration : IEntityTypeConfiguration<BeerSale>
    {

        public void Configure(EntityTypeBuilder<BeerSale> builder)
        {
            builder.ToTable("BeerSale");

            builder.HasData(
                new BeerSale()
                {
                    BeerSaleId = 1,
                    NumberOfUnits = 1000,
                    PricePerUnit = 3.99m,
                    Discount = 0,
                    SaleId = 1,
                    BeerId = 1,
                },
                new BeerSale()
                {
                    BeerSaleId = 2,
                    NumberOfUnits = 1000,
                    PricePerUnit = 8.99m,
                    Discount = 0,
                    SaleId = 1,
                    BeerId = 5,
                },
                new BeerSale()
                {
                    BeerSaleId = 3,
                    NumberOfUnits = 200,
                    PricePerUnit = 4.99m,
                    Discount = 0,
                    SaleId = 2,
                    BeerId = 2,
                },
                new BeerSale()
                {
                    BeerSaleId = 4,
                    NumberOfUnits = 300,
                    PricePerUnit = 3.99m,
                    Discount = 0,
                    SaleId = 3,
                    BeerId = 1,
                },
                new BeerSale()
                {
                    BeerSaleId = 5,
                    NumberOfUnits = 2000,
                    PricePerUnit = 3.99m,
                    Discount = 20,
                    SaleId = 3,
                    BeerId = 2,
                },
                new BeerSale()
                {
                    BeerSaleId = 6,
                    NumberOfUnits = 200,
                    PricePerUnit = 0.29m,
                    Discount = 0,
                    SaleId = 3,
                    BeerId = 4,
                },
                new BeerSale()
                {
                    BeerSaleId = 7,
                    NumberOfUnits = 200,
                    PricePerUnit = 4.99m,
                    Discount = 0,
                    SaleId = 4,
                    BeerId = 2,
                },
                new BeerSale()
                {
                    BeerSaleId = 8,
                    NumberOfUnits = 100,
                    PricePerUnit = 3.99m,
                    Discount = 0,
                    SaleId = 5,
                    BeerId = 1,
                },
                new BeerSale()
                {
                    BeerSaleId = 9,
                    NumberOfUnits = 150,
                    PricePerUnit = 4.99m,
                    Discount = 0,
                    SaleId = 5,
                    BeerId = 2,
                },
                new BeerSale()
                {
                    BeerSaleId = 10,
                    NumberOfUnits = 1000,
                    PricePerUnit = 1.39m,
                    Discount = 12,
                    SaleId = 5,
                    BeerId = 3,
                }
                );
        }
    }
}
