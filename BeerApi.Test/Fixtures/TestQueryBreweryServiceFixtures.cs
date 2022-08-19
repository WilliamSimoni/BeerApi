using Contracts.Dtos;
using Domain.Entities;
using System.Collections.Generic;

namespace BeerApi.Test.Fixtures
{
    public static class TestQueryBreweryServiceFixtures
    {

        public static IEnumerable<Brewery> GetTestBreweries()
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

        public static IEnumerable<BreweryDto> GetTestBreweryDtos()
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

        public static IEnumerable<Beer> GetTestBeers()
        {
            return new List<Beer>()
            {
                new Beer()
                {
                    BeerId = 1,
                    Name = "Beer1",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                    InProduction = true,
                    BreweryId = 1,
                    Brewery = null
                },
                new Beer()
                {
                    BeerId = 2,
                    Name = "Beer2",
                    AlcoholContent = 1,
                    SellingPriceToClients = 3,
                    SellingPriceToWholesalers = 0.56m,
                    InProduction = true,
                    BreweryId = 1,
                    Brewery = null
                },
                new Beer()
                {
                    BeerId = 3,
                    Name = "Beer3",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                    InProduction = false,
                    BreweryId = 1,
                    Brewery = null,
                    OutOfProductionDate = new DateTime(2022, 05, 09, 9, 15, 0)
                },
                new Beer()
                {
                    BeerId = 4,
                    Name = "Beer4",
                    AlcoholContent = 5.43,
                    SellingPriceToClients = 9.21m,
                    SellingPriceToWholesalers = 2,
                    InProduction = true,
                    BreweryId = 2,
                    Brewery = null
                }
            };
        }

        public static IEnumerable<BeerDto> GetTestBeerDtos()
        {
            return new List<BeerDto>()
            {
                new BeerDto()
                {
                    BeerId = 1,
                    Name = "Beer1",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4
                },
                new BeerDto()
                {
                    BeerId = 2,
                    Name = "Beer2",
                    AlcoholContent = 1,
                    SellingPriceToClients = 3,
                    SellingPriceToWholesalers = 0.56m
                },
                new BeerDto()
                {
                    BeerId = 3,
                    Name = "Beer3",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                },
                new BeerDto()
                {
                    BeerId = 4,
                    Name = "Beer4",
                    AlcoholContent = 5.43,
                    SellingPriceToClients = 9.21m,
                    SellingPriceToWholesalers = 2,
                }
            };
        }
    }
}
