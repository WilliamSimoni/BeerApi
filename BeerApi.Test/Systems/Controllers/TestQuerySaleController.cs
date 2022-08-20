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
using Mapster;
using Domain.Repositories.Specialization;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestSaleController
    {

        private readonly ILoggerManager _logger;
        public TestSaleController()
        {

            //create real logger for tests
            _logger = new Mock<ILoggerManager>().Object;
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetAll()).ReturnsAsync(SaleFixtures.GetGetSaleDtos());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetAllSales();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<List<GetSaleDto>>();
            var saleList = objectResult.Value as List<GetSaleDto>;
            saleList.Should().HaveCount(SaleFixtures.GetGetSaleDtos().Count());
        }

        [Fact]
        public async Task GetSaleById_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new GetSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetSaleById(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var objectResult = result.Result as OkObjectResult;
            objectResult.Value.Should().BeOfType<GetSaleDto>();
        }

        [Fact]
        public async Task GetSaleById_OnSaleDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

            mockServices.Setup(s => s.QuerySale).Returns(mockQuerySaleService.Object);
            mockQuerySaleService.Setup(s => s.GetBeerInvolvedInSale(It.IsAny<int>())).ReturnsAsync(new GetBeerFromSaleDto());

            var saleControler = new SaleQueryController(_logger, mockServices.Object);

            //Action
            var result = await saleControler.GetBeerInvolvedInSale(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetBeerInvolvedInSale_OnSaleDoesNotExist_ReturnNotFound()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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
        public async Task GetBeerInvolvedInSale_OnSuccess_ReturnGetBeerFromSaleDto()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQuerySaleService = new Mock<IQuerySaleServices>();

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

    }
}