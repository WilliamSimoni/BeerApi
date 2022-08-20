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

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new QueryBreweryServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
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
            brewery.AsT0.Should().Be(BreweryFixtures.GetBreweryDtos().ElementAt(breweryId-1));
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