using BeerApi.Test.Fixtures;
using Contracts.Dtos;
using Moq;
using Services.Abstract;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Domain.Common.Errors;
using Services.Abstract.UseCaseServices;
using BeerApi.Controllers;
using Domain.Logger;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestSaleQueryController
    {

        private readonly ILoggerManager _logger;
        public TestSaleQueryController()
        {

            //create real logger for tests
            _logger = new Mock<ILoggerManager>().Object;
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetAll()).ReturnsAsync(new List<GetSaleDto>());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetAllSales();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsListOfGetSaleDto()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetAll()).ReturnsAsync(new List<GetSaleDto>());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

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
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetAll()).ReturnsAsync(SaleFixtures.GetGetSaleDtos());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

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
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new GetSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsGetSaleDto()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new GetSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetSaleDto>();
        }

        //test if the controller returns the GetSaleDto returned by the service
        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsRightGetSaleDto()
        {
            //Arrange
            var getSaleDto = new GetSaleDto();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(getSaleDto);

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().Be(getSaleDto);
        }

        [Fact]
        public async Task GetSaleById_OnSaleDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new SaleNotFound(It.IsAny<int>()));

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new GetBeerFromSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSaleDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new SaleNotFound(It.IsAny<int>()));

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsGetBeerFromSaleDto()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new GetBeerFromSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetBeerFromSaleDto>();
        }

        //test if the controller returns the GetSaleDto returned by the service
        [Fact]
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnsRightGetBeerFromSaleDto()
        {
            //Arrange
            var beerFromSaleDto = new GetBeerFromSaleDto();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<ISaleQueryServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(beerFromSaleDto);

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetBeerFromSaleDto>();
            objectResult.Value.Should().Be(beerFromSaleDto);
        }
    }
}