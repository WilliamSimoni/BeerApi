using Contracts.Dtos;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BeerApi.Test.Helpers;

namespace BeerApi.Test.Systems.Services
{

    public class ValidateForUpdateInventoryBeerDtoTest
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