using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract;
using Services.Abstract.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{


    public class TestBreweryCommandController
    {
        private Mock<IServicesWrapper> servicesMock;

        private Mock<IBreweryBeersCommandServices> breweryBeersCommandServicesMock;

        private ILoggerManager loggerMock;

        public TestBreweryCommandController()
        {
            //Arrange for all tests
            
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            
            breweryBeersCommandServicesMock = new Mock<IBreweryBeersCommandServices>();
            servicesMock.Setup(s => s.ChangeBreweryBeers).Returns(breweryBeersCommandServicesMock.Object);
        }

        [Fact]
        public async Task AddBeerToBrewery_OnSuccess_Returns201Created()
        {
            //Arrange
            breweryBeersCommandServicesMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new CreatedBeerDto());

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.AddBeerToBrewery(It.IsAny<int>(), new ForCreationBeerDto());

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBreweryDoesNotExist_ReturnsNotFoundErrorMessage()
        {
            //Arrange

            breweryBeersCommandServicesMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.AddBeerToBrewery(It.IsAny<int>(), new ForCreationBeerDto());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBeerWithSameNameAlreadyExists_ReturnsConflictErrorMessage()
        {
            //Arrange

            breweryBeersCommandServicesMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new BreweryBeerConflict(It.IsAny<string>(), It.IsAny<int>()));

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.AddBeerToBrewery(It.IsAny<int>(), new ForCreationBeerDto());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnSuccess_ReturnsNoContent()
        {
            //Arrange

            breweryBeersCommandServicesMock.Setup(s => s.RemoveBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.removeBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBreweryNotFound_ReturnsNotFound()
        {
            //Arrange

            breweryBeersCommandServicesMock.Setup(s => s.RemoveBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.removeBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task RemoveBeerFromBrewery_OnBeerNotFound_ReturnsNotFound()
        {
            //Arrange

            breweryBeersCommandServicesMock.Setup(s => s.RemoveBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new BreweryBeerNotFound(It.IsAny<int>(), It.IsAny<int>()));

            var controller = new BreweryCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.removeBeerFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}