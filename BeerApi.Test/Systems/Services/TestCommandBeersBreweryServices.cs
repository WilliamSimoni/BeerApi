using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Logger;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{


    public class TestCommandBreweryBeersServices
    {
        private BreweryBeersCommandServices service;

        public TestCommandBreweryBeersServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = new Mapper();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new BreweryBeersCommandServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task AddBeerToBrewery_OnSuccess_ReturnsCreatedBeer()
        {
            var newBeer = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };

            //Action
            var result = await service.AddBeerToBrewery(1, newBeer);

            //Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.Should().Be(newBeer.Adapt<BeerDto>());
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBreweryDoesNotExist_ReturnsIErrorWith404Number()
        {
            //Action
            var result = await service.AddBeerToBrewery(5, new ForCreationBeerDto());

            //Assert
            result.IsT0.Should().BeFalse();
            result.AsT1.Number.Should().Be(404);
        }

        [Fact]
        public async Task AddBeerToBrewery_OnNameConflict_ReturnsIErrorWith409Number()
        {
            var newBeer = new ForCreationBeerDto()
            {
                Name = "Beer1",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };

            //Action
            var result = await service.AddBeerToBrewery(1, newBeer);

            //Assert
            result.IsT0.Should().BeFalse();
            result.AsT1.Number.Should().Be(409);
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnSuccess_ReturnsNull()
        {
            //Action (remove beer with id 1 from brewery with id 1)
            var result = await service.RemoveBeerFromBrewery(1,1);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBreweryDoesNotExist_ReturnsIErrorWithCode404()
        {
            //Action (remove beer with id 1 from brewery with id 5)
            var result = await service.RemoveBeerFromBrewery(5, 1);

            //Assert
            result.Number.Should().Be(404);
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBeerDoesNotExist_ReturnsIErrorWithCode404()
        {
            //Action (remove beer with id 100 from brewery with id 1)
            var result = await service.RemoveBeerFromBrewery(1, 100);

            //Assert
            result.Number.Should().Be(404);
        }
    }
}