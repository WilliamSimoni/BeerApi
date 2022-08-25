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

    public class TestSaleQueryServices
    {

        private SaleQueryServices service;

        public TestSaleQueryServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new SaleQueryServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task GetAll_OnSuccess_ReturnsListOfGetSaleDto()
        {
            // Action
            var serviceResult = await service.GetAll();
            // Assert
            serviceResult.Should().BeOfType<GetSaleDto[]>();
        }

        [Fact]
        public async Task GetAll_OnSuccess_ReturnsAllTheSales()
        {
            // Action
            var serviceResult = await service.GetAll();
            // Assert
            serviceResult.Should().HaveCount(SaleFixtures.GetGetSaleDtos().Count());
        }

        [Fact]
        public async Task GetAll_OnSuccess_ReturnsCorrectListOfSales()
        {
            // Action
            var serviceResult = await service.GetAll();
            // Assert
            serviceResult.Should().Equal(SaleFixtures.GetGetSaleDtos());
        }

        [Fact]
        public async Task GetById_OnSuccess_ReturnsGetSaleDto()
        {
            // Action
            var serviceResult = await service.GetById(1);
            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetSaleDto>();
        }

        [Fact]
        public async Task GetById_OnSuccess_ReturnsCorrectSale()
        {
            // Action
            var serviceResult = await service.GetById(1);
            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should()
                .BeEquivalentTo(SaleFixtures.GetGetSaleDtos().Where(s => s.SaleId == 1).First());
        }

        [Fact]
        public async Task GetById_OnSaleNotFound_ReturnsSaleNotFoundError()
        {
            // Action
            var serviceResult = await service.GetById(1000);
            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<SaleNotFound>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsGetBeerFromDto()
        {
            // Action
            var serviceResult = await service.GetBeerInvolvedInSale(1);
            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<GetBeerFromSaleDto>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsCorrectBeer()
        {
            // Action
            var serviceResult = await service.GetBeerInvolvedInSale(1);
            // Assert
            serviceResult.IsT1.Should().BeFalse();
            //Sale 1 is associated with Beer with Id 1
            serviceResult.AsT0.Should()
                .BeEquivalentTo(SaleFixtures.GetGetBeerFromSaleDtos().Where(b => b.BeerId == 1).First());
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSaleNotFound_ReturnsSaleNotFoundError()
        {
            // Action
            var serviceResult = await service.GetById(1000);
            // Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<SaleNotFound>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnBeerNotInProduction_ReturnsCorrectBeer()
        {
            // Action Sale 2 is associated with Beer with Id 3 (which is not in production)
            var serviceResult = await service.GetBeerInvolvedInSale(2);
            // Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should()
                .Be(SaleFixtures.GetGetBeerFromSaleDtos().Where(b => b.BeerId == 3).First());
        }


    }
}
