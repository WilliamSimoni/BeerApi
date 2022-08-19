using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Repositories.Configurations
{
    public class WholesalerConfiguration : IEntityTypeConfiguration<Wholesaler>
    {

        public void Configure(EntityTypeBuilder<Wholesaler> builder)
        {
            builder.ToTable("Wholesaler");

            builder.HasData(
                new Wholesaler()
                {
                    WholesalerId = 1,
                    Name = "thebeer",
                    Address = "jump street 21",
                    Email = "info@thebeer.be"
                },
                new Wholesaler()
                {
                    WholesalerId = 2,
                    Name = "berallax corp",
                    Address = "evergreen street 32",
                    Email = "contact@berallaxcorp.com"
                },
                new Wholesaler()
                {
                    WholesalerId=3,
                    Name = "the beer corporation",
                    Address = "sesame street 77",
                    Email = "thebeercorporationinfo@beercorp.com"
                }
                );
        }
    }
}
