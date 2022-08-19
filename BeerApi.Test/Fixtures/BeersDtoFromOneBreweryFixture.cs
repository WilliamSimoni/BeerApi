using Contracts.Dtos;
using Domain.Entities;
using System.Collections.Generic;

namespace BeerApi.Test.Fixtures
{
    public static class BeersDtoFromOneBreweryFixture
    {

        public static List<BeerDto> GetTestData()
        {
            return new List<BeerDto>()
            {
                new BeerDto
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
                    SellingPriceToWholesalers = 4
                }
            };
        }

    }
}