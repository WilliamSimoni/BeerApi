using Contracts.Dtos;
using Domain.Entities;

namespace BeerApi.Test.Fixtures
{
    public static class BreweryFixtures
    {

        public static IEnumerable<Brewery> GetBreweries()
        {
            return new List<Brewery>()
            {
                new Brewery
                {
                    BreweryId = 1,
                    Name = "Brewery1",
                    Address = "Address1",
                    Email = "Email1",
                    Beers = null
                },
                new Brewery()
                {
                    BreweryId = 2,
                    Name = "Brewery2",
                    Address = "Address2",
                    Email = "Email2",
                    Beers = null
                },
                new Brewery()
                {
                    BreweryId = 3,
                    Name = "Brewery3",
                    Address = "Address3",
                    Email = "Email3",
                    Beers = null
                }
            };
        }

        public static IEnumerable<BreweryDto> GetBreweryDtos()
        {
            return new List<BreweryDto>()
            {
                new BreweryDto
                {
                    BreweryId = 1,
                    Name = "Brewery1",
                    Address = "Address1",
                    Email = "Email1",
                },
                new BreweryDto()
                {
                    BreweryId = 2,
                    Name = "Brewery2",
                    Address = "Address2",
                    Email = "Email2",
                },
                new BreweryDto()
                {
                    BreweryId = 3,
                    Name = "Brewery3",
                    Address = "Address3",
                    Email = "Email3",
                }
            };
        }
    }
}
