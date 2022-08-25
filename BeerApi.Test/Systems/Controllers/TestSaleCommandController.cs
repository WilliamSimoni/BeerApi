using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Services.Abstract;
using Services.Abstract.UseCaseServices;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestSaleCommandController
    {
        private readonly ILoggerManager loggerMock;

        private Mock<IServicesWrapper> servicesMock;

        private Mock<ISaleCommandServices> saleCommandServicesMock;
        public TestSaleCommandController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            saleCommandServicesMock = new Mock<ISaleCommandServices>();

            servicesMock.Setup(s => s.ChangeSale).Returns(saleCommandServicesMock.Object);
        }

        [Fact]
        public async Task InsertSale_OnSuccess_ReturnsStatusCode201()
        {
            //Arrange
            saleCommandServicesMock.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new CreatedSaleDto());

            var saleControler = new SaleCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task InsertSale_OnSuccess_ReturnsCreatedSaleDto()
        {
            //Arrange
            saleCommandServicesMock.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new CreatedSaleDto());

            var saleControler = new SaleCommandController(loggerMock, servicesMock.Object);

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

            saleCommandServicesMock.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(createdSaleDto);

            var saleControler = new SaleCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            var createdResult = result as CreatedResult;
            createdResult.Value.Should().BeEquivalentTo(createdSaleDto);
        }

        [Fact]
        public async Task InsertSale_WholesalerOnNotFound_NotReturnsSuccessCode()
        {
            //Arrange
            saleCommandServicesMock.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var saleControler = new SaleCommandController(loggerMock, servicesMock.Object);

            saleControler.ProblemDetailsFactory = new Mock<ProblemDetailsFactory>().Object;

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().NotBeOfType<CreatedResult>();
        }

        [Fact]
        public async Task InsertSale_BeerNotFound_NotReturnsSuccessCode()
        {
            //Arrange
            saleCommandServicesMock.Setup(s => s.addSale(It.IsAny<ForCreationSaleDto>()))
                .ReturnsAsync(new BeerNotFound(It.IsAny<int>()));

            var saleControler = new SaleCommandController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.PostSale(new ForCreationSaleDto());

            //Assert
            result.Should().NotBeOfType<CreatedResult>();
        }
    }
}
