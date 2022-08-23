using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract.UseCaseServices;
using Services.Abstract;
using FluentAssertions;
using Domain.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestSaleCommandController
    {
        private readonly ILoggerManager _logger;
        public TestSaleCommandController()
        {
            //create real logger for tests
            _logger = new Mock<ILoggerManager>().Object;
        }

        [Fact]
        public async Task InsertSale_OnSuccess_ReturnsStatusCode201()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockSaleCommandService = new Mock<ISaleCommandServices>();

            mockServices.Setup(s => s.ChangeSale).Returns(mockSaleCommandService.Object);
            mockSaleCommandService.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new CreatedSaleDto());

            var saleControler = new SaleCommandController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task InsertSale_OnSuccess_ReturnsCreatedSaleDto()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockSaleCommandService = new Mock<ISaleCommandServices>();

            mockServices.Setup(s => s.ChangeSale).Returns(mockSaleCommandService.Object);
            mockSaleCommandService.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new CreatedSaleDto());

            var saleControler = new SaleCommandController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            var createdResult = result as CreatedResult;
            createdResult.Value.Should().BeOfType<CreatedSaleDto>();
        }

        //test if the controller returns the same object returned by the server layer
        [Fact]
        public async Task InsertSale_OnSuccess_ReturnsRightCreatedSaleDto()
        {
            //Arrange
            var createdSaleDto = new CreatedSaleDto();

            var mockServices = new Mock<IServicesWrapper>();
            var mockSaleCommandService = new Mock<ISaleCommandServices>();

            mockServices.Setup(s => s.ChangeSale).Returns(mockSaleCommandService.Object);
            mockSaleCommandService.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(createdSaleDto);

            var saleControler = new SaleCommandController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            var createdResult = result as CreatedResult;
            createdResult.Value.Should().Be(createdSaleDto);
        }

        [Fact]
        public async Task InsertSale_WholesalerOnNotFound_ReturnsObjectResult()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockSaleCommandService = new Mock<ISaleCommandServices>();

            mockServices.Setup(s => s.ChangeSale).Returns(mockSaleCommandService.Object);
            mockSaleCommandService.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var saleControler = new SaleCommandController(_logger, mockServices.Object);

            saleControler.ProblemDetailsFactory = new Mock<ProblemDetailsFactory>().Object;

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().BeOfType<ObjectResult>();
        }

        [Fact]
        public async Task InsertSale_BeerNotFound_ReturnsObjectResult()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockSaleCommandService = new Mock<ISaleCommandServices>();

            mockServices.Setup(s => s.ChangeSale).Returns(mockSaleCommandService.Object);
            mockSaleCommandService.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new BeerNotFound(It.IsAny<int>()));

            var saleControler = new SaleCommandController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().BeOfType<ObjectResult>();
        }
    }
}
