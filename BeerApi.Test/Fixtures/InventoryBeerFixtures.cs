using Contracts.Dtos;
using Domain.Entities;

namespace BeerApi.Test.Fixtures
{
    public static class InventoryBeerFixtures
    {
        public static IEnumerable<InventoryBeer> GetInventoryBeers()
        {
            return new List<InventoryBeer>()
            {
                new InventoryBeer()
                {
                    InventoryBeerId = 1,
                    Quantity = 250,
                    BeerId = 1,
                    WholesalerId = 1
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 4,
                    Quantity = 50,
                    BeerId = 2,
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
                }
            };
        }

        public static IEnumerable<InventoryBeer> GetInventoryBeersWithInfo()
        {
            return new List<InventoryBeer>()
            {
                new InventoryBeer()
                {
                    InventoryBeerId = 1,
                    Quantity = 250,
                    BeerId = 1,
                    WholesalerId = 1,
                    Beer = BeerFixtures.GetBeers().Where(b => b.BeerId == 1).First()
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 4,
                    Quantity = 50,
                    BeerId = 2,
                    WholesalerId = 1,
                    Beer = BeerFixtures.GetBeers().Where(b => b.BeerId == 2).First()
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 2,
                    Quantity = 30,
                    BeerId = 3,
                    WholesalerId = 2,
                    Beer = BeerFixtures.GetBeers().Where(b => b.BeerId == 3).First()
                },
                new InventoryBeer()
                {
                    InventoryBeerId = 3,
                    Quantity = 70,
                    BeerId = 2,
                    WholesalerId = 2,
                    Beer = BeerFixtures.GetBeers().Where(b => b.BeerId == 2).First()
                }
            };
        }

        public static IEnumerable<GetInventoryBeerDto> GetGetInventoryBeerDtos()
        {
            var beers = BeerFixtures.GetBeers();

            return new List<GetInventoryBeerDto>()
            {
                new GetInventoryBeerDto()
                {
                    BeerId = 1,
                    BreweryId = beers.Where(b => b.BeerId == 1).First().BreweryId,
                    Name = beers.Where(b => b.BeerId == 1).First().Name,
                    AlcoholContent = beers.Where(b => b.BeerId == 1).First().AlcoholContent,
                    SellingPrice = beers.Where(b => b.BeerId == 1).First().SellingPriceToClients,
                    Quantity = 250,
                },
                new GetInventoryBeerDto()
                {
                    BeerId = 2,
                    BreweryId = beers.Where(b => b.BeerId == 2).First().BreweryId,
                    Name = beers.Where(b => b.BeerId == 2).First().Name,
                    AlcoholContent = beers.Where(b => b.BeerId == 2).First().AlcoholContent,
                    SellingPrice = beers.Where(b => b.BeerId == 2).First().SellingPriceToClients,
                    Quantity = 70,
                },
                new GetInventoryBeerDto()
                {
                    BeerId = 3,
                    BreweryId = beers.Where(b => b.BeerId == 3).First().BreweryId,
                    Name = beers.Where(b => b.BeerId == 3).First().Name,
                    AlcoholContent = beers.Where(b => b.BeerId == 3).First().AlcoholContent,
                    SellingPrice = beers.Where(b => b.BeerId == 3).First().SellingPriceToClients,
                    Quantity = 30,
                },
                new GetInventoryBeerDto()
                {
                    BeerId = 2,
                    BreweryId = beers.Where(b => b.BeerId == 2).First().BreweryId,
                    Name = beers.Where(b => b.BeerId == 2).First().Name,
                    AlcoholContent = beers.Where(b => b.BeerId == 2).First().AlcoholContent,
                    SellingPrice = beers.Where(b => b.BeerId == 2).First().SellingPriceToClients,
                    Quantity = 70,
                }
            };
        }
    }
}
