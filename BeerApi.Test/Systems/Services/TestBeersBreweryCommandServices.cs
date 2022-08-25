using BeerApi.Test.Helpers;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using Domain.Repositories;
using FluentAssertions;
using Mapster;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{


    public class TestBreweryBeersCommandServices
    {
        private BreweryBeersCommandServices service;

        private Mock<IUnitOfWork> unitOfWorkMock;
        public TestBreweryBeersCommandServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            unitOfWorkMock = UnitOfWorkMock.Get();

            service = new BreweryBeersCommandServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        private ForCreationBeerDto GetForCreationBeerDto()
        {
            return new ForCreationBeerDto()
            {
                Name = "test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
        }

        [Fact]
        public async Task AddBeerToBrewery_OnSuccess_ReturnsCreatedBeerDto()
        {
            //Arrange
            var newBeerDto = GetForCreationBeerDto();

            //Action
            var result = await service.AddBeerToBrewery(1, newBeerDto);

            //Assert
            result.IsT0.Should().BeTrue();

            result.AsT0.Should().Be(newBeerDto.Adapt<CreatedBeerDto>());
        }

        [Fact]
        public async Task AddBeerToBrewery_OnSuccess_CallsSaveAsync()
        {
            //Arrange
            var newBeerDto = GetForCreationBeerDto();

            //Action
            var result = await service.AddBeerToBrewery(1, newBeerDto);

            //Assert
            //Assert
            unitOfWorkMock.Verify(mock => mock.SaveAsync(), Times.Once());
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBreweryDoesNotExist_ReturnsBreweryNotFound()
        {
            //Action
            var result = await service.AddBeerToBrewery(5, new ForCreationBeerDto());

            //Assert
            result.IsT0.Should().BeFalse();
            result.AsT1.Should().BeOfType<BreweryNotFound>();
        }

        [Fact]
        public async Task AddBeerToBrewery_OnNameConflict_ReturnsBreweryBeerConflictError()
        {
            //Arrange
            var newBeerDto = GetForCreationBeerDto();
            newBeerDto.Name = "beer1";

            //Action
            var result = await service.AddBeerToBrewery(1, newBeerDto);

            //Assert
            result.IsT0.Should().BeFalse();
            result.AsT1.Should().BeOfType<BreweryBeerConflict>();
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnSuccess_ReturnsNull()
        {
            //Action (remove beer with id 1 from brewery with id 1)
            var result = await service.RemoveBeerFromBrewery(1, 1);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBreweryDoesNotExist_ReturnsBreweryNotFoundError()
        {
            //Action (remove beer with id 1 from brewery with id 5)
            var result = await service.RemoveBeerFromBrewery(5, 1);

            //Assert
            result.Should().BeOfType<BreweryNotFound>();
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBeerDoesNotExist_ReturnsBeerNotFoundError()
        {
            //Action (remove beer with id 100 from brewery with id 1)
            var result = await service.RemoveBeerFromBrewery(1, 100);

            //Assert
            result.Should().BeOfType<BreweryBeerNotFound>();
        }
    }
}