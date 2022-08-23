using BeerApi.Test.Helpers.Mocks;
using BeerApi.Test.Helpers;
using Domain.Logger;
using Moq;
using Services.UseCaseServices;
using Services.Abstract.UseCaseServices;
using Contracts.Dtos;
using FluentAssertions;
using Domain.Entities;
using Domain.Common.Errors;
using BeerApi.Test.Fixtures;

namespace BeerApi.Test.Systems.Services
{
    public class TestWholesalerCommandService
    {
        private WholesalerCommandServices service;

        private ForUpdateInventoryBeerDto correctUpdateBeerDto;

        public TestWholesalerCommandService()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new WholesalerCommandServices(loggerMock.Object, unitOfWorkMock.Object, mapper);

            correctUpdateBeerDto = new ForUpdateInventoryBeerDto() { 
                Quantity = 20
            };
        }

        [Fact]
        public async Task UpdateQuantity_OnSuccess_ReturnsUpdatedInventoryBeerDto()
        {
            //Action
            var serviceResult = await service.UpdateQuantity(1, 1, correctUpdateBeerDto);

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<UpdatedInventoryBeerDto>();
        }

        [Fact]
        public async Task UpdateQuantity_OnSuccess_ReturnsUpdatedInventoryBeerDtoWithCorrectQuantity()
        {
            //Action
            var serviceResult = await service.UpdateQuantity(1, 1, correctUpdateBeerDto);

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Quantity.Should().Be(correctUpdateBeerDto.Quantity);
        }

        [Fact]
        public async Task UpdateQuantity_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            //Action
            var serviceResult = await service.UpdateQuantity(10, 1, correctUpdateBeerDto);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

        [Fact]
        public async Task UpdateQuantity_OnBeerNotFound_ReturnsBeerNotSoldByWholesalerError()
        {
            //Action
            var serviceResult = await service.UpdateQuantity(1, 9, correctUpdateBeerDto);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<BeerNotSoldByWholesaler>();
        }

        [Fact]
        public async Task UpdateQuantity_OnSuccess_CalledSaveAsync()
        {
            //Arrange
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            var service = new WholesalerCommandServices(loggerMock.Object, unitOfWorkMock.Object, mapper);

            //Action
            var serviceResult = await service.UpdateQuantity(1, 1, correctUpdateBeerDto);

            //Assert
            unitOfWorkMock.Verify(mock => mock.SaveAsync(), Times.Once());
        }

    }
}
