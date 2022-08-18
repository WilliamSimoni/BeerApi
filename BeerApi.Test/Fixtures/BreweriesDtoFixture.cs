using Contracts.Dtos;
using Domain.Entities;
using System.Collections.Generic;

namespace BeerApi.Test.Fixtures
{
    public static class BreweriesDtoFixture
    {

        public static IEnumerable<BreweryDto> getTestData()
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
                    BreweryId = 2,
                    Name = "Brewery2",
                    Address = "Address2",
                    Email = "Email2",
                }
            };
        }

    }
}
