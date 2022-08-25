using BeerApi.Controllers;
using BeerApi.Test.Fixtures;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract;
using Services.Abstract.UseCaseServices;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestBreweryQueryController
    {

        private Mock<IServicesWrapper> servicesMock;

        private Mock<IBreweryBeersQueryServices> breweryBeersQueryServicesMock;

        private Mock<IBreweryQueryServices> breweryQueryServicesMock;

        private ILoggerManager loggerMock;

        public TestBreweryQueryController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();

            breweryQueryServicesMock = new Mock<IBreweryQueryServices>();
            breweryBeersQueryServicesMock = new Mock<IBreweryBeersQueryServices>();

            servicesMock.Setup(s => s.QueryBrewery).Returns(breweryQueryServicesMock.Object);
            servicesMock.Setup(s => s.QueryBreweryBeers).Returns(breweryBeersQueryServicesMock.Object);
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            breweryQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(new List<BreweryDto>());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_ReturnsListOfBreweries()
        {
            //Arrange
            breweryQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(new List<BreweryDto>());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var breweries = result as OkObjectResult;
            breweries.Value.Should().BeOfType<List<BreweryDto>>();
        }

        [Fact]
        public async void GetAllBreweries_OnSuccess_GetAllTheBreweries()
        {
            //Arrange
            IEnumerable<BreweryDto> breweriesFixture = BreweryFixtures.GetBreweryDtos();

            breweryQueryServicesMock.Setup(s => s.GetAll()).ReturnsAsync(breweriesFixture);

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBreweries();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var breweries = result as OkObjectResult;
            breweries.Value.Should().BeOfType<List<BreweryDto>>();
            var breweries_values = breweries.Value as IEnumerable<BreweryDto>;
            breweries_values.Count().Should().Be(breweriesFixture.Count());
        }

        [Fact]
        public async void GetBreweryById_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            breweryQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new BreweryDto());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBreweryById(1);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBreweryById_OnBreweryNotFound_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            breweryQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBreweryById(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetBreweryById_OnSuccess_ReturnsCorrectBrewery()
        {
            //Arrange
            IEnumerable<BreweryDto> breweriesFixture = BreweryFixtures.GetBreweryDtos();

            breweryQueryServicesMock.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(breweriesFixture.ElementAt(0));

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBreweryById(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var brewery = result as OkObjectResult;
            brewery.Value.Should().BeOfType<BreweryDto>();
            var brewery_values = brewery.Value as BreweryDto;
            brewery_values.Should().Be(breweriesFixture.ElementAt(0));
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsStatusCose200()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new List<BeerDto>());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnBreweryIdIsNotCorrect_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsListOfBeers()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(new List<BeerDto>());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var beers = result as OkObjectResult;
            beers.Value.Should().BeOfType<List<BeerDto>>();
        }

        [Fact]
        public async void GetAllBeersFromBrewery_OnSuccess_ReturnsAllBeers()
        {
            //Arrange
            var breweryId = 1;
            var beersFixture = BeerFixtures.GetBeers()
                .Where(b => b.BreweryId == breweryId)
                .Adapt<BeerDto[]>();

            breweryBeersQueryServicesMock.Setup(s => s.GetAllBeers(It.IsAny<int>())).ReturnsAsync(beersFixture);

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetAllBeersFromBrewery(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var beers = result as OkObjectResult;
            beers.Value.Should().BeOfType<BeerDto[]>();
            var beersList = beers.Value as BeerDto[];
            beersList.Count().Should().Be(beersFixture.Count());
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnSuccess_ReturnsStatusCose200()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new BeerDto());

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnBreweryDoesNotExist_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new BreweryNotFound(It.IsAny<int>()));

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetBeerByIfFromBrewery_OnBeerDoesNotExist_ReturnsObjectResultWithStatus404()
        {
            //Arrange
            breweryBeersQueryServicesMock.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(new BreweryBeerNotFound(It.IsAny<int>(), It.IsAny<int>()));

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), It.IsAny<int>());

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetBeerFromBrewery_OnSuccess_ReturnsRightBeer(int beerId)
        {
            //Arrange
            var expectedBeer = BeerFixtures.GetBeers()
                .Where(b => b.BeerId == beerId)
                .Adapt<BeerDto[]>().First();

            breweryBeersQueryServicesMock.Setup(s => s.GetBeerById(It.IsAny<int>(), It.IsAny<int>())).
                ReturnsAsync(expectedBeer);

            var bc = new BreweryQueryController(loggerMock, servicesMock.Object);

            //Action
            var result = await bc.GetBeerByIdFromBrewery(It.IsAny<int>(), beerId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var beer = result as OkObjectResult;
            beer.Value.Should().BeOfType<BeerDto>();
            var beerValue = beer.Value as BeerDto;
            beerValue.Should().Be(expectedBeer);
        }
    }
}