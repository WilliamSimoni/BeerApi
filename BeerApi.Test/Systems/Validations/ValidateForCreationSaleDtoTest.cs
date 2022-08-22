using Contracts.Dtos;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BeerApi.Test.Helpers;

namespace BeerApi.Test.Systems.Services
{

    public class ValidateForCreationSaleDtoTest
    {


        [Fact]
        public void ModelState_OnCorrectDto_ReturnsTrue()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 1000,
                PricePerUnit = 3.99m,
                Discount = 0,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeTrue();
        }

        [Fact]
        public void ModelState_OnNegativeNumberOfUnits_ReturnsFalse()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = -5,
                PricePerUnit = 3.99m,
                Discount = 0,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeSellingPrice_ReturnsFalse()
        {
            // Action
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 1000,
                PricePerUnit = -5m,
                Discount = 0,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnZeroNumberOfUnits_ReturnsFalse()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 0,
                PricePerUnit = 3.99m,
                Discount = 0,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnZeroPrice_ReturnsFalse()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 1000,
                PricePerUnit = 0,
                Discount = 0,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }


        [Fact]
        public void ModelState_OnNegativeDiscount_ReturnsFalse()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 1000,
                PricePerUnit = 3.99m,
                Discount = -1,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }


        [Fact]
        public void ModelState_OnDiscountLargerThan100_ReturnsFalse()
        {
            // Arrange
            var dto = new ForCreationSaleDto()
            {
                NumberOfUnits = 1000,
                PricePerUnit = 3.99m,
                Discount = 101,
                WholesalerId = 1,
                BeerId = 1,
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(dto);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

    }
}