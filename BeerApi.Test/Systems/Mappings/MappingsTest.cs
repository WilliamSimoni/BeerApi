using Contracts.Dtos;
using FluentAssertions;
using BeerApi.Test.Helpers;
using Domain.Entities;
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
            mapping.Name.Should().Be(expected.Name);
            mapping.AlcoholContent.Should().Be(expected.AlcoholContent);
            mapping.SellingPriceToClients.Should().Be(expected.SellingPriceToClients);
            mapping.SellingPriceToWholesalers.Should().Be(expected.SellingPriceToWholesalers);
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
            mapping.BeerId.Should().Be(expected.BeerId);
            mapping.BreweryId.Should().Be(expected.BreweryId);
            mapping.Name.Should().Be(expected.Name);
            mapping.AlcoholContent.Should().Be(expected.AlcoholContent);
            mapping.SellingPrice.Should().Be(expected.SellingPrice);
            mapping.Quantity.Should().Be(expected.Quantity);

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
            mapping.BeerId.Should().Be(expected.BeerId);
            mapping.RequestedQuantity.Should().Be(expected.RequestedQuantity);
            mapping.PricePerUnit.Should().Be(expected.PricePerUnit);
            mapping.SubTotal.Should().Be(expected.SubTotal);
        }

        //We test the mapping from (QuoteRequestDto, ICollection<QuoteSummaryItemDto, decimal, decimal) to QuoteSummaryDto
        [Fact]
        public void Mapper_OnMapFrom_QuoteReqDtoAndListOfQuoteSummaryItemDtoAndDiscountAndTotal_ToQuoteSummaryDto_ReturnsExpectedResut()
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

            decimal sourceDiscount = 20;
            decimal sourceTotal = 110;

            QuoteSummaryDto expected = new QuoteSummaryDto()
            {
                WholesalerId = 4,
                Total = 88,
                TotalWithoutDiscount = 110,
                AppliedDiscount = 20,
                Beers = sourceQuoteItems
            };

            //Action
            var mapping = mapper.Map<QuoteSummaryDto>((sourceQuoteRequest, sourceQuoteItems, sourceDiscount, sourceTotal));

            // Assert
            mapping.WholesalerId.Should().Be(expected.WholesalerId);
            mapping.Total.Should().Be(expected.Total);
            mapping.TotalWithoutDiscount.Should().Be(expected.TotalWithoutDiscount);
            mapping.AppliedDiscount.Should().Be(expected.AppliedDiscount);

            //check if the beer lists contain the same elements
            mapping.Beers.Should().HaveCount(expected.Beers.Count());
            foreach (var beers in expected.Beers.Zip(mapping.Beers, Tuple.Create))
            {
                beers.Item1.BeerId.Should().Be(beers.Item2.BeerId);
                beers.Item1.RequestedQuantity.Should().Be(beers.Item2.RequestedQuantity);
                beers.Item1.PricePerUnit.Should().Be(beers.Item2.PricePerUnit);
                beers.Item1.SubTotal.Should().Be(beers.Item2.SubTotal);
            }
        }

        [Fact]
        public void Mapper_OnMapFromForCreationSaleDtoToSale()
        {
            // Arrange
            ForCreationSaleDto source = new ForCreationSaleDto()
            {
                WholesalerId = 1,
                BeerId = 1,
                NumberOfUnits = 10,
                PricePerUnit = 50,
                Discount = 10
            };

            Sale expected = new Sale()
            {
                SaleDate = DateTime.Now,
                NumberOfUnits = 10,
                PricePerUnit = 50,
                Discount = 10,
                Total = 450,
                WholesalerId = 1,
                BeerId = 1
            };

            //Action
            var mapping = mapper.Map<Sale>(source);

            // Assert
            //expected and mapping can not have the same date.
            //So, I just tested that expected has an older date than the mapped object.
            mapping.SaleDate.Should().BeAfter(expected.SaleDate);   

            mapping.NumberOfUnits.Should().Be(expected.NumberOfUnits);
            mapping.PricePerUnit.Should().Be(expected.PricePerUnit);
            mapping.Discount.Should().Be(expected.Discount);
            mapping.Total.Should().Be(expected.Total);
            mapping.WholesalerId.Should().Be(expected.WholesalerId);
            mapping.BeerId.Should().Be(expected.BeerId);
        }
    }
}
