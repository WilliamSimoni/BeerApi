using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract;
using Services.Abstract.UseCaseServices;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestWholesalerQueryController
    {
        private ILoggerManager loggerMock;
        private Mock<IServicesWrapper> servicesMock;
        private Mock<IWholesalerQueryServices> wholesalerQueryServicesMock;

        public TestWholesalerQueryController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            wholesalerQueryServicesMock = new Mock<IWholesalerQueryServices>();

            servicesMock.Setup(s => s.QueryWholesaler).Returns(wholesalerQueryServicesMock.Object);
        }

        [Fact]
        public async Task GetAllBeers_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetAllWholesalerBeers(It.IsAny<int>()))
                .ReturnsAsync(new List<GetInventoryBeerDto>());

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetAllBeers(It.IsAny<int>());
            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetAllBeers_OnSuccess_ReturnsGetInventoryBeerDto()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetAllWholesalerBeers(It.IsAny<int>()))
                .ReturnsAsync(new List<GetInventoryBeerDto>());

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetAllBeers(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<List<GetInventoryBeerDto>>();
        }

        [Fact]
        public async Task GetAllBeers_OnWholesalerNotFound_ReturnsStatusCode404()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetAllWholesalerBeers(It.IsAny<int>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetAllBeers(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerById_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetWholesalerBeerById(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new GetInventoryBeerDto());

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetBeerById(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBeerByID_OnSuccess_ReturnsGetInventoryBeerDto()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetWholesalerBeerById(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new GetInventoryBeerDto());

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetBeerById(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<GetInventoryBeerDto>();
        }

        [Fact]
        public async Task GetBeerByID_OnWholesalerNotFound_ReturnsStatusCode404()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetWholesalerBeerById(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetBeerById(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerByID_OnBeerNotFound_ReturnsStatusCode404()
        {
            //Arrange

            wholesalerQueryServicesMock.Setup(s => s.GetWholesalerBeerById(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new BeerNotFound(It.IsAny<int>()));

            var controller = new WholesalerQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.GetBeerById(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }
    }
}
