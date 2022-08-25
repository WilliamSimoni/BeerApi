using BeerApi.Controllers;
using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract.UseCaseServices;
using Services.Abstract;
using FluentAssertions;
using Domain.Entities;
using Domain.Common.Errors;

namespace BeerApi.Test.Systems.Controllers
{
    public class TestQuoteController
    {
        private ILoggerManager loggerMock;
        private Mock<IServicesWrapper> servicesMock;
        private Mock<IQuoteServices> quoteServicesMock;

        public TestQuoteController()
        {
            //Arrange for all tests

            loggerMock = new Mock<ILoggerManager>().Object;

            servicesMock = new Mock<IServicesWrapper>();
            quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);
        }

        [Fact]
        public async Task CreateQuote_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuoteSummaryDto());

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateQuote_OnSuccess_ReturnsQuoteSummaryDto()
        {
            //Arrange

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuoteSummaryDto());

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<QuoteSummaryDto>();
        }

        [Fact]
        public async Task CreateQuote_OnWholesalerNotFound_ReturnsErroMessage()
        {
            //Arrange

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }

        public async Task CreateQuote_OnBeerNotFound_ReturnsErroMessage()
        {
            //Arrange

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new BeerNotSoldByWholesaler(It.IsAny<int>(), It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }

        public async Task CreateQuote_OnQuantityOverUnitsInStock_ReturnsErroMessage()
        {
            //Arrange

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuantityOverUnitsInStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }
    }
}
