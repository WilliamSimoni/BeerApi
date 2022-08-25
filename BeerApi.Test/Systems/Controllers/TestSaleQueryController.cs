using BeerApi.Controllers;
using BeerApi.Test.Fixtures;
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
    public class TestSaleQueryController
    {

        private ILoggerManager loggerMock;

        private Mock<IServicesWrapper> servicesMock;

        private Mock<ISaleQueryServices> saleQueryServicesMock;


        public TestSaleQueryController()
        {

            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            saleQueryServicesMock = new Mock<ISaleQueryServices>();

            servicesMock.Setup(s => s.QuerySale).Returns(saleQueryServicesMock.Object);
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(new List<GetSaleDto>());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetAllSales();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsListOfGetSaleDto()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(new List<GetSaleDto>());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetAllSales();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<List<GetSaleDto>>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsAllSales()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(SaleFixtures.GetGetSaleDtos());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetAllSales();

            //Assert
            var objectResult = result.Result as OkObjectResult;
            var saleList = objectResult.Value as List<GetSaleDto>;
            saleList.Should().HaveCount(SaleFixtures.GetGetSaleDtos().Count());
        }

        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new GetSaleDto());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsGetSaleDto()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new GetSaleDto());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetSaleDto>();
        }

        //test if the controller returns the GetSaleDto returned by the service
        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsRightGetSaleDto()
        {
            //Arrange
            var getSaleDto = new GetSaleDto();

            saleQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(getSaleDto);

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().Be(getSaleDto);
        }

        [Fact]
        public async Task GetSaleById_OnSaleDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new SaleNotFound(It.IsAny<int>()));

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new GetBeerFromSaleDto());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSaleDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new SaleNotFound(It.IsAny<int>()));

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsGetBeerFromSaleDto()
        {
            //Arrange
            saleQueryServicesMock.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new GetBeerFromSaleDto());

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetBeerFromSaleDto>();
        }

        //test if the controller returns the GetSaleDto returned by the service
        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsRightGetBeerFromSaleDto()
        {
            //Arrange
            var beerFromSaleDto = new GetBeerFromSaleDto();

            saleQueryServicesMock.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(beerFromSaleDto);

            var saleControler = new SaleQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetBeerFromSaleDto>();
            objectResult.Value.Should().Be(beerFromSaleDto);
        }
    }
}