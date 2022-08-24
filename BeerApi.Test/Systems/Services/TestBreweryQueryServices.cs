using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers;
using BeerApi.Test.Helpers.Mocks;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{


    public class TestBreweryQueryServices
    {
        private BreweryQueryServices service;

        public TestBreweryQueryServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new BreweryQueryServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task GetAllBreweries_OnSuccess_ReturnsAllBreweries()
        {
            //Action
            var breweries = await service.GetAll();

            //Assert
            breweries.Should().HaveCount(BreweryFixtures.GetBreweryDtos().Count());
        }

        [Fact]
        public async Task GetAllBreweries_OnSuccess_ReturnsAllBReweriesWithCorrectData()
        {
            //Action
            var breweries = await service.GetAll();

            //Assert
            breweries.Should().Equal(BreweryFixtures.GetBreweryDtos());
        }

        [Fact]
        public async Task GetById_OnSuccess_ReturnsCorrectBrewery()
        {
            //Action
            int breweryId = 1;
            var brewery = await service.GetById(breweryId);

            //Assert
            brewery.IsT0.Should().BeTrue();
            brewery.AsT0.Should().Be(BreweryFixtures.GetBreweryDtos().ElementAt(breweryId - 1));
        }

        [Fact]
        public async Task GetById_OnNotFound_ReturnsIErrorWith404Number()
        {
            //Action
            var brewery = await service.GetById(5);

            //Assert
            brewery.IsT0.Should().BeFalse();
            brewery.AsT1.Should().BeOfType<BreweryNotFound>();
        }
    }
}