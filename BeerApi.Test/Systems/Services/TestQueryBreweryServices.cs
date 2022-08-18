using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers.Mocks;
using Domain.Entities;
using Domain.Logger;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using FluentAssertions;
using MapsterMapper;
using Moq;
using Services.UseCaseServices;
using System.Linq.Expressions;

namespace BeerApi.Test.Systems.Services
{


    public class TestQueryBreweryServices
    {
        private QueryBreweryServices service;

        public TestQueryBreweryServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = new Mapper();

            var unitOfWorkMock = UnitOfWorkMockForBreweryServices.GetMock();

            service = new QueryBreweryServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task GetAllBreweries_OnSuccess_ReturnsAllBreweries()
        {
            //Action
            var breweries = await service.GetAll();

            //Assert
            breweries.Should().HaveCount(BreweriesDtoFixture.GetTestData().Count());
        }

        [Fact]
        public async Task GetAllBreweries_OnSuccess_ReturnsAllBReweriesWithCorrectData()
        {
            //Action
            var breweries = await service.GetAll();

            //Assert
            breweries.Should().Equal(TestQueryBreweryServiceFixtures.GetTestBreweryDtos());
        }

        [Fact]
        public async Task GetById_OnSuccess_ReturnsCorrectBrewery()
        {
            //Action
            var brewery = await service.GetById(1);

            //Assert
            brewery.IsT0.Should().BeTrue();
            brewery.AsT0.Should().Be(TestQueryBreweryServiceFixtures.GetTestBreweryDtos().ElementAt(0));
        }

        [Fact]
        public async Task GetById_OnNotFound_ReturnsIErrorWith404Number()
        {
            //Action
            var brewery = await service.GetById(5);

            //Assert
            brewery.IsT0.Should().BeFalse();
            brewery.AsT1.Number.Should().Be(404);
        }
    }
}