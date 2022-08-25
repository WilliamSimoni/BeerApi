using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services

{
    public class TestWholesalerQueryServices
    {
        private WholesalerQueryServices service;

        public TestWholesalerQueryServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new WholesalerQueryServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task GetAllWholesalerBeers_OnSuccess_ReturnsListOfGetInventoryBeerDto()
        {
            // Action
            var serviceResult = await service.GetAllWholesalerBeers(1);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetInventoryBeerDto[]>();
        }

        [Fact]
        public async Task GetAllWholesalerBeers_OnSuccess_ReturnsAllTheBeersSoldByTheWholesaler()
        {
            // Action
            var serviceResult = await service.GetAllWholesalerBeers(2);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            // Wholesaler with id 2 sells two beers
            serviceResult.AsT0.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllWholesalerBeers_OnSuccess_ReturnsCorrectBeers()
        {
            // Action
            var serviceResult = await service.GetAllWholesalerBeers(2);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            // Wholesaler beers are the two last in the IEnumerable returned by InventoryBeerFixtures
            serviceResult.AsT0.Should()
                .Equal(InventoryBeerFixtures.GetGetInventoryBeerDtos().TakeLast(2));
        }

        [Fact]
        public async Task GetAllWholesalerBeers_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            // Action
            var serviceResult = await service.GetAllWholesalerBeers(1000);

            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

        [Fact]
        public async Task GetWholesalerBeerById_OnSuccess_ReturnsGetInventoryBeerDto()
        {
            // Action
            var serviceResult = await service.GetWholesalerBeerById(1, 1);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetInventoryBeerDto>();
        }

        [Fact]
        public async Task GetWholesalerBeerById_OnSuccess_ReturnsCorrectGetInventoryBeerDto()
        {
            // Action
            var serviceResult = await service.GetWholesalerBeerById(1, 1);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            //Wholesaler beer with id 1 is the first one in the IEnumerable returned by InventoruBeersFixtures
            serviceResult.AsT0.Should().BeEquivalentTo(InventoryBeerFixtures.GetGetInventoryBeerDtos().ElementAt(0));
        }

        public async Task GetWholesalerBeerById_OnBeerIsNotInProductionButIsInTheTable_ReturnsGetInventoryBeerDto()
        {
            // Action (Beer3 is not in production)
            var serviceResult = await service.GetWholesalerBeerById(2, 3);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetInventoryBeerDto>();
        }

        [Fact]
        public async Task GetWholesalerBeerById_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            // Action
            var serviceResult = await service.GetWholesalerBeerById(1000, 1);

            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

        [Fact]
        public async Task GetWholesalerBeerById_OnBeerNotFound_ReturnsBeerNotSoldByWholesalerError()
        {
            // Action
            var serviceResult = await service.GetWholesalerBeerById(1, 1000);

            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<BeerNotSoldByWholesaler>();
        }

        [Fact]
        public async Task GetAllWholesalers_OnSuccess_ReturnsListOfGetWholesalerDto()
        {
            // Action
            var serviceResult = await service.GetAllWholesalers();

            // Assert
            serviceResult.Should().BeOfType<GetWholesalerDto[]>();
        }

        [Fact]
        public async Task GetAllWholesalers_OnSuccess_ReturnsAllWholesalers()
        {
            // Action
            var serviceResult = await service.GetAllWholesalers();

            // Assert
            serviceResult.Should().HaveCount(WholesalerFixtures.GetWholesalers().Count());
        }

        [Fact]
        public async Task GetAllWholesalers_OnSuccess_ReturnsCorrectWholesalers()
        {
            // Action
            var serviceResult = await service.GetAllWholesalers();

            // Assert
            serviceResult.Should().Equal(WholesalerFixtures.GetGetWholesalerDtos());
        }

        [Fact]
        public async Task GetWholesalerById_OnSuccess_ReturnsGetWholesalerDto()
        {
            // Action
            var serviceResult = await service.GetWholesalerById(1);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetWholesalerDto>();
        }

        [Fact]
        public async Task GetWholesalerById_OnSuccess_ReturnsCorrectGetWholesalerDto()
        {
            // Action
            var serviceResult = await service.GetWholesalerById(1);

            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeEquivalentTo(WholesalerFixtures.GetGetWholesalerDtos().First());
        }

        [Fact]
        public async Task GetWholesalerById_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            // Action
            var serviceResult = await service.GetWholesalerById(1000);

            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

    }
}
