using BeerApi.Test.Helpers;
using Contracts.Dtos;
using Domain.Entities;
using FluentAssertions;
using MapsterMapper;

namespace BeerApi.Test.Systems.Mappings
{
    public class MappingsTest
    {
        private IMapper mapper;

        public MappingsTest()
        {
            mapper = MapperInstance.Get();
        }

        [Fact]
        public void Mapper_OnMapFrom_ForCreationBeerDto_To_Beer_ReturnsExpectedResut()
        {
            // Arrange
            ForCreationBeerDto source = new ForCreationBeerDto()
            {
                Name = "TeSt",
                AlcoholContent = 5,
                SellingPriceToClients = 10.54353453m,
                SellingPriceToWholesalers = 10
            };

            Beer expected = new Beer()
            {
                Name = "test",
                AlcoholContent = 5,
                SellingPriceToClients = 10.54m,
                SellingPriceToWholesalers = 10,
            };

            // Action
            var mapping = mapper.Map<Beer>(source);

            // Assert
            mapping.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Mapper_OnMapFrom_InventoryBeer_To_GetInventoryBeerDto_ReturnsExpectedResult()
        {
            // Arrange
            InventoryBeer source = new InventoryBeer()
            {
                InventoryBeerId = 1,
                Quantity = 10,
                BeerId = 4,
                WholesalerId = 5,
                Beer = new Beer()
                {
                    BeerId = 4,
                    Name = "test",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10.54m,
                    SellingPriceToWholesalers = 10,
                    BreweryId = 7
                }
            };

            GetInventoryBeerDto expected = new GetInventoryBeerDto()
            {
                BeerId = 4,
                BreweryId = 7,
                Name = "test",
                AlcoholContent = 5,
                SellingPrice = 10.54m,
                Quantity = 10
            };

            // Action
            var mapping = mapper.Map<GetInventoryBeerDto>(source);

            // Assert
            mapping.Should().BeEquivalentTo(expected);

        }

        [Fact]
        public void Mapper_OnMapFrom_QuoteReqDtoAndInventoryBeer_To_QuoteSummaryItemDto_ReturnsExpectedResut()
        {
            // Arrange
            InventoryBeer sourceBeer = new InventoryBeer()
            {
                InventoryBeerId = 1,
                Quantity = 10,
                BeerId = 4,
                WholesalerId = 5,
                Beer = new Beer()
                {
                    BeerId = 4,
                    Name = "test",
                    AlcoholContent = 5,
                    SellingPriceToClients = 10.54m,
                    SellingPriceToWholesalers = 10,
                    BreweryId = 7
                }
            };

            QuoteRequestItemDto sourceQuote = new QuoteRequestItemDto()
            {
                BeerId = 4,
                Quantity = 7
            };

            QuoteSummaryItemDto expected = new QuoteSummaryItemDto()
            {
                BeerId = 4,
                RequestedQuantity = 7,
                PricePerUnit = 10.54m,
                SubTotal = 73.78m
            };

            // Action
            var mapping = mapper.Map<QuoteSummaryItemDto>((sourceQuote, sourceBeer));

            // Assert
            mapping.Should().BeEquivalentTo(expected);
        }

        //We test the mapping from (QuoteRequestDto, ICollection<QuoteSummaryItemDto, decimal, decimal) to QuoteSummaryDto
        [Theory]
        [InlineData(0, 110, 110)]
        [InlineData(10, 110, 99)]
        [InlineData(50, 110, 55)]
        public void Mapper_OnMapFrom_QuoteReqDtoAndListOfQuoteSummaryItemDtoAndDiscountAndTotal_ToQuoteSummaryDto_ReturnsExpectedResut(decimal appliedDiscount, decimal totalWithoutDiscount, decimal expectedTotal)
        {

            QuoteRequestDto sourceQuoteRequest = new QuoteRequestDto()
            {
                WholesalerId = 4,
                Beers = new List<QuoteRequestItemDto>
                {
                    new QuoteRequestItemDto()
                    {
                        BeerId = 4,
                        Quantity = 70
                    },
                    new QuoteRequestItemDto()
                    {
                        BeerId = 10,
                        Quantity = 20
                    }
                }
            };

            ICollection<QuoteSummaryItemDto> sourceQuoteItems = new List<QuoteSummaryItemDto>()
                {
                    new QuoteSummaryItemDto()
                {
                    BeerId = 4,
                    RequestedQuantity = 70,
                    PricePerUnit = 1,
                    SubTotal = 70
                },
                new QuoteSummaryItemDto()
                {
                    BeerId = 10,
                    RequestedQuantity = 20,
                    PricePerUnit = 2,
                    SubTotal = 40
                }
            };

            decimal sourceDiscount = appliedDiscount;
            decimal sourceTotal = totalWithoutDiscount;

            QuoteSummaryDto expected = new QuoteSummaryDto()
            {
                WholesalerId = 4,
                Total = expectedTotal,
                TotalWithoutDiscount = totalWithoutDiscount,
                AppliedDiscount = appliedDiscount,
                Beers = sourceQuoteItems
            };

            //Action
            var mapping = mapper.Map<QuoteSummaryDto>((sourceQuoteRequest, sourceQuoteItems, sourceDiscount, sourceTotal));

            // Assert
            mapping.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 500)]
        [InlineData(10, 450)]
        [InlineData(20, 400)]
        public void Mapper_OnMapFromForCreationSaleDtoToSale(int appliedDiscount, decimal expectedTotal)
        {
            // Arrange
            ForCreationSaleDto source = new ForCreationSaleDto()
            {
                WholesalerId = 1,
                BeerId = 1,
                NumberOfUnits = 10,
                PricePerUnit = 50,
                Discount = appliedDiscount
            };

            Sale expected = new Sale()
            {
                SaleDate = DateTime.Now,
                NumberOfUnits = 10,
                PricePerUnit = 50,
                Discount = appliedDiscount,
                Total = expectedTotal,
                WholesalerId = 1,
                BeerId = 1
            };

            //Action
            var mapping = mapper.Map<Sale>(source);

            // Assert
            //expected and mapping can not have the same date.
            //So, I just tested that expected has an older date than the mapped object.
            mapping.SaleDate.Should().BeAfter(expected.SaleDate);
            mapping.SaleDate = expected.SaleDate;

            mapping.Should().BeEquivalentTo(expected);
        }
    }
}
