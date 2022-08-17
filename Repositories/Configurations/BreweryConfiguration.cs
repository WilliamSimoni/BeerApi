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
                    Name = "Huisbrouwerij De Halve Maan",
                    Address = "Walplein 26 8000 Brugge",
                    Email = "info@halvemaan.be"
                },
                new Brewery
                {
                    BreweryId = 2,
                    Name = "Bourgogne des Flandres",
                    Address = "Kartuizerinnenstraat 6 8000 Brugge",
                    Email = "visits@bourgognedesflandres"
                }
                );
        }
    }
}
