using Contracts.Dtos;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BeerApi.Test.Helpers;

namespace BeerApi.Test.Systems.Services
{

    public class ValidateForCreationBeerDtoTest
    {
        [Fact]
        public void ModelState_OnCorrectDto_ReturnTrue()
        {
            //Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            
            //Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            //Assert
            isModelStateValid.Should().BeTrue();
        }

        [Fact]
        public void ModelState_OnTooLongName_ReturnsFalse()
        {
            // Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);


            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeSellingPriceToClients_ReturnsFalse()
        {
            // Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = -1,
                SellingPriceToWholesalers = 1
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_SellingPriceToClientsIs0_ReturnsFalse()
        {
            // Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 0,
                SellingPriceToWholesalers = 1
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            // Assert 
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeSellingPriceToWholesalers_ReturnsFalse()
        {
            // Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = -1
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_SellingPriceToWholesalersIs0_ReturnsFalse()
        {
            //Arrange 
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 0
            };

            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_AlcoholContentIsNegative_ReturnsFalse()
        {
            // Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = -1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            
            // Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            // Assert
            isModelStateValid.Should().BeFalse();
        }

        

        [Fact]
        public void ModelState_AlcoholContentIsLargerThan100_ReturnsFalse()
        {
            //Arrange
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1000,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };

            //Action
            var isModelStateValid = ValidationTestHelper.Validate(sut);

            //Assert
            isModelStateValid.Should().BeFalse();
        }
    }
}