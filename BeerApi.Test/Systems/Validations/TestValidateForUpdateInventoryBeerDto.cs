using BeerApi.Test.Helpers;
using Contracts.Dtos;
using FluentAssertions;

namespace BeerApi.Test.Systems.Services
{

    public class TestValidateForUpdateInventoryBeerDto
    {


        [Fact]
        public void ModelState_OnCorrectDto_ReturnsTrue()
        {
            // Arrange
            var dto = new ForUpdateInventoryBeerDto()
            {
                Quantity = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeTrue();
        }

        [Fact]
        public void ModelState_OnNegativeQuantity_ReturnsFalse()
        {
            // Arrange
            var dto = new ForUpdateInventoryBeerDto()
            {
                Quantity = -1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

    }
}