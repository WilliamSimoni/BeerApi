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
using Xunit;

namespace BeerApi.Test.Systems.Services
{


    public class TestQueryBreweryBeersServices
    {
        private QueryBreweryBeersServices service;

        public TestQueryBreweryBeersServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = new Mapper();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new QueryBreweryBeersServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task GetAllBeers_OnBreweryDoesNotExist_ReturnsIErrorWith404Number()
        {
            //Action
            var result = await service.GetAllBeers(5);

            //Assert
            result.IsT0.Should().BeFalse();
            result.AsT1.Number.Should().Be(404);
        }


        [Theory]
        //For Brewery with id = 1, the beers in the dataset are 3, but only two of them
        //are still in production. So, the correct result is 2.
        [InlineData(1, 2)]
        [InlineData(2, 1)]
        public async Task GetAllBeers_OnBreweryExists_ReturnsAllBreweryBeers(int breweryId, int expectedResult)
        {
            //Action
            var result = await service.GetAllBeers(breweryId);

            //Assert
            result.IsT0.Should().BeTrue();
            result.AsT0.Should().HaveCount(expectedResult);
        }

        [Fact]
        public async Task GetAllBeers_OnBreweryExists_ReturnsCorrectBeers()
        {
            //Action
            var result = await service.GetAllBeers(2);

            //Assert
            result.IsT0.Should().BeTrue();
            //Beer with id 4 is the only beer associated to the brewery with id 2
            result.AsT0.Should().Equal(BeerFixtures.GetBeersDto().Where(b => b.BeerId == 4));
        }
    }
}