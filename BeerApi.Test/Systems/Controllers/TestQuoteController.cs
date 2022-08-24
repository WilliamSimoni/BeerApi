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

        public TestQuoteController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;
        }

        [Fact]
        public async Task CreateQuote_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            var servicesMock = new Mock<IServicesWrapper>();
            var quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuoteSummaryDto());

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task CreateQuote_OnSuccess_ReturnsQuoteSummaryDto()
        {
            //Arrange
            var servicesMock = new Mock<IServicesWrapper>();
            var quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuoteSummaryDto());

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result.Result as OkObjectResult;
            okObjectResult.Value.Should().BeOfType<QuoteSummaryDto>();
        }

        [Fact]
        public async Task CreateQuote_OnWholesalerNotFound_ReturnsErroMessage()
        {
            //Arrange
            var servicesMock = new Mock<IServicesWrapper>();
            var quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new WholesalerNotFound(It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }

        public async Task CreateQuote_OnBeerNotFound_ReturnsErroMessage()
        {
            //Arrange
            var servicesMock = new Mock<IServicesWrapper>();
            var quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new BeerNotSoldByWholesaler(It.IsAny<int>(), It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }

        public async Task CreateQuote_OnQuantityOverUnitsInStock_ReturnsErroMessage()
        {
            //Arrange
            var servicesMock = new Mock<IServicesWrapper>();
            var quoteServicesMock = new Mock<IQuoteServices>();

            servicesMock.Setup(s => s.AskQuote).Returns(quoteServicesMock.Object);

            quoteServicesMock.Setup(s => s.GetQuote(It.IsAny<QuoteRequestDto>()))
                .ReturnsAsync(new QuantityOverUnitsInStock(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));

            var controller = new QuoteController(loggerMock, servicesMock.Object);

            //Action
            var result = await controller.CreateQuote(It.IsAny<QuoteRequestDto>());

            //Assert
            result.Result.Should().NotBeOfType<OkObjectResult>();
            var objectResult = result.Result as ObjectResult;
            objectResult.Value.Should().BeOfType<ValidationProblemDetails>();
            var validationProblems = objectResult.Value as ValidationProblemDetails;
            validationProblems.Errors.Should().HaveCount(1);

        }
    }
}
