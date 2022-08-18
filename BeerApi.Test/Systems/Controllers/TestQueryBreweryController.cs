using BeerApi.Test.Fixtures;
using Contracts.Dtos;
using Moq;
using Services.Abstract;
using Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Domain.Common.Errors;

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
            var breweries = result.Result as OkObjectResult;
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
        public async void GetBreweryById_OnBreweryNotFound_ReturnsStatusCode404()
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
            result.Result.Should().BeOfType<NotFoundObjectResult>();
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
            var brewery = result.Result as OkObjectResult;
            brewery.Value.Should().BeOfType<BreweryDto>();
            var brewery_values = brewery.Value as BreweryDto;
            brewery_values.Should().Be(breweriesFixture.ElementAt(0));
        }
    }
}