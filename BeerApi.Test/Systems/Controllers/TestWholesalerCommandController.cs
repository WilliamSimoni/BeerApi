using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract.UseCaseServices;
using Services.Abstract;
using FluentAssertions;
using Domain.Common.Errors;
using OneOf.Types;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestWholesalerCommandController
    {
        private ILoggerManager loggerMock;
        private Mock<IServicesWrapper> servicesMock;
        private Mock<IWholesalerCommandServices> wholesalerCommandServicesMock;

        public TestWholesalerCommandController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            wholesalerCommandServicesMock = new Mock<IWholesalerCommandServices>();

            servicesMock.Setup(s => s.ChangeWholesaler).Returns(wholesalerCommandServicesMock.Object);
        }

        [Fact]
        public async Task UpdateBeerQuantity_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange

            wholesalerCommandServicesMock.Setup(s => s.UpdateQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>()))
                .ReturnsAsync(new UpdatedInventoryBeerDto());

            var controller = new WholesalerCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.UpdateBeerQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task UpdateBeerQuantity_OnSuccess_ReturnsUpdatedInventoryBeerDto()
        {
            //Arrange

            wholesalerCommandServicesMock.Setup(s => s.UpdateQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>()))
                .ReturnsAsync(new UpdatedInventoryBeerDto());

            var controller = new WholesalerCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.UpdateBeerQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<UpdatedInventoryBeerDto>();
        }

        public async Task UpdateBeerQuantity_OnWholesalerNotFound_ReturnsStatusCode404()
        {
            //Arrange

            wholesalerCommandServicesMock.Setup(s => s.UpdateQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var controller = new WholesalerCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.UpdateBeerQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);

        }

        public async Task UpdateBeerQuantity_OnBeerNotFound_ReturnsStatusCode404()
        {
            //Arrange

            wholesalerCommandServicesMock.Setup(s => s.UpdateQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>()))
                .ReturnsAsync(new BeerNotFound(It.IsAny<int>()));

            var controller = new WholesalerCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.UpdateBeerQuantity(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<ForUpdateInventoryBeerDto>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}
