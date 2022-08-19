using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Repositories.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {

        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sale");

            builder.HasData(
                new Sale()
                {
                    SaleId = 1,
                    SaleDate = new DateTime(2021, 10, 04, 18, 0, 0),
                    Total = 12980,
                    WholesalerId = 1,
                },
                new Sale()
                {
                    SaleId = 2,
                    SaleDate = new DateTime(2022, 01, 02, 12, 30, 0),
                    Total = 998,
                    WholesalerId = 2
                },
                new Sale()
                {
                    SaleId = 3,
                    SaleDate = new DateTime(2022, 08, 06, 18, 0, 0),
                    Total = 9235,
                    WholesalerId = 1,
                },
                new Sale()
                {
                    SaleId = 4,
                    SaleDate = new DateTime(2022, 02, 03, 16, 0, 0),
                    Total = 998,
                    WholesalerId = 2
                },
                new Sale()
                {
                    SaleId = 5,
                    SaleDate = new DateTime(2022, 02, 03, 16, 0, 0),
                    Total = 2537.5m,
                    WholesalerId = 3,
                }
                );
        }
    }
}
