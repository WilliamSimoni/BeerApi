using Contracts.Dtos;
using FluentAssertions;
using BeerApi.Test.Helpers;
using Domain.Entities;

namespace BeerApi.Test.Systems.Services
{

    public class ValidateQuoteRequestDtoTest
    {

        [Fact]
        public void ModelState_OnCorrectDto_ReturnsTrue()
        {
            // Arrange
            var dto = new QuoteRequestDto()
            {
                WholesalerId = 1,
                Beers = new List<QuoteRequestItemDto>()
                {
                    new QuoteRequestItemDto()
                    {
                        BeerId = 1,
                        Quantity = 10
                    },
                    new QuoteRequestItemDto()
                    {
                        BeerId = 2,
                        Quantity = 10
                    },
                }
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeTrue();
        }

        [Fact]
        public void ModelState_OnDuplicateBeer_ReturnsFalse()
        {
            // Arrange
            var dto = new QuoteRequestDto()
            {
                WholesalerId = 1,
                Beers = new List<QuoteRequestItemDto>()
                {
                    new QuoteRequestItemDto()
                    {
                        BeerId = 1,
                        Quantity = 10
                    },
                    new QuoteRequestItemDto()
                    {
                        BeerId = 1,
                        Quantity = 10
                    },
                }
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeBeerQuantity_ReturnsFalse()
        {
            // Arrange
            var dto = new QuoteRequestItemDto()
            {
                BeerId = 1,
                Quantity = -10
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnEmptyBeerList_ReturnsFalse()
        {
            // Arrange
            var dto = new QuoteRequestDto()
            {
                WholesalerId = 1,
                Beers = new List<QuoteRequestItemDto>()
                {
                }
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

    }
}