using BeerApi.Test.Fixtures;
using Contracts.Dtos;
using Moq;
using Services.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Domain.Common.Errors;
using Services.Abstract.UseCaseServices;
using BeerApi.Controllers;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestQueryBreweryController
    {

        private readonly ILogger<BreweryQueryController> _logger;

        public TestQueryBreweryController()
        {

            //create real logger for tests
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var _logger = factory.CreateLogger<BreweryQueryController>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();
            
            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetAll()).ReturnsAsync(new List<BreweryDto>());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsListOfBreweries()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();

            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetAll()).ReturnsAsync(new List<BreweryDto>());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var breweries = result.Result as OkObjectResult;
            breweries.Value.Should().BeOfType<List<BreweryDto>>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_GetAllTheBreweries()
        {
            //Arrange
            IEnumerable<BreweryDto> breweriesFixture = BreweriesDtoFixture.getTestData();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();

            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetAll()).ReturnsAsync(breweriesFixture);

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var breweries = result.Result as OkObjectResult;
            breweries.Value.Should().BeOfType<List<BreweryDto>>();
            var breweries_values = breweries.Value as IEnumerable<BreweryDto>;
            breweries_values.Count().Should().Be(breweriesFixture.Count());
        }

        [Fact]
        public async void GetBreweryById_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();

            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new BreweryDto());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBreweryById(1);

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBreweryById_OnBreweryNotFound_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();

            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBreweryById(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetBreweryById_OnSuccess_ReturnsCorrectBrewery()
        {
            //Arrange
            IEnumerable<BreweryDto> breweriesFixture = BreweriesDtoFixture.getTestData();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryServices = new Mock<IQueryBreweryServices>();

            mockServices.Setup(s => s.QueryBrewery).Returns(mockQueryBreweryServices.Object);
            mockQueryBreweryServices.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(breweriesFixture.ElementAt(0));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBreweryById(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var brewery = result.Result as OkObjectResult;
            brewery.Value.Should().BeOfType<BreweryDto>();
            var brewery_values = brewery.Value as BreweryDto;
            brewery_values.Should().Be(breweriesFixture.ElementAt(0));
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsStatusCose200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new List<BeerDto>());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnBreweryIdIsNotCorrect_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsListOfBeers()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new List<BeerDto>());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var beers = result.Result as OkObjectResult;
            beers.Value.Should().BeOfType<List<BeerDto>>();
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsAllBeers()
        {
            //Arrange
            var beersFixture = BeersDtoFromOneBreweryFixture.getTestData();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(beersFixture);

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var beers = result.Result as OkObjectResult;
            beers.Value.Should().BeOfType<List<BeerDto>>();
            var beersList = beers.Value as List<BeerDto>;
            beersList.Count().Should().Be(beersFixture.Count());
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnSuccess_ReturnsStatusCose200()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new BeerDto());

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnBreweryDoesNotExist_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnBeerDoesNotExist_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new BreweryBeerNotFound(It.IsAny<int>(), It.IsAny<int>()));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Result.Should().BeOfType<ObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnSuccess_ReturnsRightBeer()
        {
            //Arrange
            var beersFixture = BeersDtoFromOneBreweryFixture.getTestData();

            var mockServices = new Mock<IServicesWrapper>();
            var mockQueryBreweryBeersServices = new Mock<IQueryBreweryBeersServices>();

            mockServices.Setup(s => s.QueryBreweryBeers).Returns(mockQueryBreweryBeersServices.Object);
            mockQueryBreweryBeersServices.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(beersFixture.ElementAt(0));

            var bc = new BreweryQueryController(_logger, mockServices.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), 0);

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var beer = result.Result as OkObjectResult;
            beer.Value.Should().BeOfType<BeerDto>();
            var beerValue = beer.Value as BeerDto;
            beerValue.Should().Be(beersFixture.ElementAt(0));
        }

    }
}