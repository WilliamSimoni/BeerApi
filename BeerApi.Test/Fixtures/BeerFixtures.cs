using Contracts.Dtos;
using Domain.Entities;

namespace BeerApi.Test.Fixtures
{
    public static class BeerFixtures
    {
        public static IEnumerable<Beer> GetBeers()
        {
            return new List<Beer>()
            {
                new Beer()
                {
                    BeerId = 1,
                    Name = "beer1",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                    InProduction = true,
                    BreweryId = 1,
                },
                new Beer()
                {
                    BeerId = 2,
                    Name = "beer2",
                    AlcoholContent = 1,
                    SellingPriceToClients = 3,
                    SellingPriceToWholesalers = 0.56m,
                    InProduction = true,
                    BreweryId = 1,
                },
                new Beer()
                {
                    BeerId = 3,
                    Name = "beer3",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                    InProduction = false,
                    BreweryId = 1,
                    OutOfProductionDate = new DateTime(2022, 05, 09, 9, 15, 0)
                },
                new Beer()
                {
                    BeerId = 4,
                    Name = "beer4",
                    AlcoholContent = 5.43,
                    SellingPriceToClients = 9.21m,
                    SellingPriceToWholesalers = 2,
                    InProduction = true,
                    BreweryId = 2,
                }
            };
        }

        public static IEnumerable<BeerDto> GetBeersDto()
        {
            return new List<BeerDto>()
            {
                new BeerDto()
                {
                    BeerId = 1,
                    Name = "beer1",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4
                },
                new BeerDto()
                {
                    BeerId = 2,
                    Name = "beer2",
                    AlcoholContent = 1,
                    SellingPriceToClients = 3,
                    SellingPriceToWholesalers = 0.56m
                },
                new BeerDto()
                {
                    BeerId = 3,
                    Name = "beer3",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                },
                new BeerDto()
                {
                    BeerId = 4,
                    Name = "beer4",
                    AlcoholContent = 5.43,
                    SellingPriceToClients = 9.21m,
                    SellingPriceToWholesalers = 2,
                }
            };
        }

        public static IEnumerable<GetBeerFromSaleDto> GetBeersFromSaleDto()
        {
            return new List<GetBeerFromSaleDto>()
            {
                new GetBeerFromSaleDto()
                {
                    BeerId = 1,
                    BreweryId = 1,
                    Name = "beer1",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4
                },
                new GetBeerFromSaleDto()
                {
                    BeerId = 2,
                    BreweryId = 1,
                    Name = "beer2",
                    AlcoholContent = 1,
                    SellingPriceToClients = 3,
                    SellingPriceToWholesalers = 0.56m
                },
                new GetBeerFromSaleDto()
                {
                    BeerId = 3,
                    BreweryId = 1,
                    Name = "beer3",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10,
                    SellingPriceToWholesalers = 4,
                },
                new GetBeerFromSaleDto()
                {
                    BeerId = 4,
                    BreweryId = 2,
                    Name = "beer4",
                    AlcoholContent = 5.43,
                    SellingPriceToClients = 9.21m,
                    SellingPriceToWholesalers = 2,
                }
            };
        }
    }
}
