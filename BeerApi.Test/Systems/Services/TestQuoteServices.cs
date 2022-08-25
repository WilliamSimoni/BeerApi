using BeerApi.Test.Helpers;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using FluentAssertions;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{

    public class TestQuoteServices
    {
        private QuoteServices service;

        private QuoteRequestDto correctQuoteRequestDto;

        public TestQuoteServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            var unitOfWorkMock = UnitOfWorkMock.Get();

            service = new QuoteServices(loggerMock.Object, unitOfWorkMock.Object, mapper);

            correctQuoteRequestDto = new QuoteRequestDto()
            {
                WholesalerId = 1,
                Beers = new List<QuoteRequestItemDto>
                {
                    new QuoteRequestItemDto()
                    {
                        BeerId = 1,
                        Quantity = 5
                    }
                }
            };
        }

        [Fact]
        public async Task GetQuote_OnSuccess_ReturnsQuoteSummaryDto()
        {
            //Action
            var serviceResult = await service.GetQuote(correctQuoteRequestDto);

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<QuoteSummaryDto>();
        }

        [Theory]
        [InlineData(5, 0)]
        [InlineData(10, 10)]
        [InlineData(20, 20)]
        public async Task GetQuote_OnSuccess_ReturnsQuoteSummaryDtoWithExpectedDiscount(int quantity, decimal expectedDiscount)
        {
            //Arrange
            var quoteRequest = correctQuoteRequestDto;
            //set the quantity of the first item in the list of required beers
            quoteRequest.Beers.ElementAt(0).Quantity = quantity;

            //Action
            var serviceResult = await service.GetQuote(quoteRequest);

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.AppliedDiscount.Should().Be(expectedDiscount);
        }

        [Fact]
        public async Task GetQuote_OnSuccess_ReturnsQuoteSummaryDtoWithExpectedTotal()
        {
            //Arrange
            var quoteRequest = correctQuoteRequestDto;
            //Add a beer to the quoteRequest
            quoteRequest.Beers.Add(new QuoteRequestItemDto()
            {
                BeerId = 2,
                Quantity = 1
            });

            //Action
            var serviceResult = await service.GetQuote(quoteRequest);

            //Assert
            //The total number of ordered beers is 6; therefore, the discount will be 0
            //ExpectedTotal = 10 * 5 + 3 * 1 = 53
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.TotalWithoutDiscount.Should().Be(53);
            serviceResult.AsT0.Total.Should().Be(53);
        }

        [Fact]
        public async Task GetQuote_OnSuccess_ReturnsQuoteSummaryItems()
        {
            //Action
            var serviceResult = await service.GetQuote(correctQuoteRequestDto);

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Beers.Should().Equal(new List<QuoteSummaryItemDto>()
            {
                new QuoteSummaryItemDto()
                {
                    BeerId=1,
                    PricePerUnit = 10,
                    SubTotal = 50,
                    RequestedQuantity = 5
                }
            });
        }

        [Fact]
        public async Task GetQuote_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            //Arrange
            var quoteRequest = correctQuoteRequestDto;
            quoteRequest.WholesalerId = 1000;

            //Action
            var serviceResult = await service.GetQuote(quoteRequest);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

        [Fact]
        public async Task GetQuote_OnBeerNotFound_ReturnsBeerNotSoldByWholesalerError()
        {
            //Arrange
            var quoteRequest = correctQuoteRequestDto;
            quoteRequest.Beers.ElementAt(0).BeerId = 1000;

            //Action
            var serviceResult = await service.GetQuote(quoteRequest);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<BeerNotSoldByWholesaler>();
        }

        [Fact]
        public async Task GetQuote_OnNotEnoughBeerInStock_ReturnsQuantityOverUnitsInStockError()
        {
            //Arrange
            var quoteRequest = correctQuoteRequestDto;
            quoteRequest.Beers.ElementAt(0).Quantity = 1000;

            //Action
            var serviceResult = await service.GetQuote(quoteRequest);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<QuantityOverUnitsInStock>();
        }

    }
}
