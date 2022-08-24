using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Configurations
{
    public class InventoryBeerConfiguration : IEntityTypeConfiguration<InventoryBeer>
    {
        public void Configure(EntityTypeBuilder<InventoryBeer> builder)
        {
            builder.ToTable("InventoryBeer");

            builder.HasData(
                new InventoryBeer()
                {
                    InventoryBeerId = 1,
                    Quantity = 250,
                    BeerId = 1,
                    WholesalerId = 1
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 2,
                    Quantity = 30,
                    BeerId = 2,
                    WholesalerId = 2
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 3,
                    Quantity = 70,
                    BeerId = 1,
                    WholesalerId = 2
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 4,
                    Quantity = 12,
                    BeerId = 5,
                    WholesalerId = 3
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 5,
                    Quantity = 500,
                    BeerId = 4,
                    WholesalerId = 3
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 6,
                    Quantity = 437,
                    BeerId = 3,
                    WholesalerId = 3
                }
                );
        }
    }
}
