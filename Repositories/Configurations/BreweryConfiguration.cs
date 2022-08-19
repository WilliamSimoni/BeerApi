using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Repositories.Configurations
{
    public class BreweryConfiguration : IEntityTypeConfiguration<Brewery>
    {

        public void Configure(EntityTypeBuilder<Brewery> builder)
        {
            builder.ToTable("Brewery");

            builder.HasData(
                new Brewery
                {
                    BreweryId = 1,
                    Name = "huisbrouwerij de halve maan",
                    Address = "walplein 26 8000 brugge",
                    Email = "info@halvemaan.be"
                },
                new Brewery
                {
                    BreweryId = 2,
                    Name = "bourgogne des flandres",
                    Address = "kartuizerinnenstraat 6 8000 brugge",
                    Email = "visits@bourgognedesflandres"
                }
                );
        }
    }
}
