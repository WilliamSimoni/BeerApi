using Contracts.Dtos;
using FluentAssertions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BeerApi.Test.Systems.Services
{

    public class ValidateForCreationBeerDtoTest
    {
        [Fact]
        public void ModelState_OnCorrectDto_ReturnTrue()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeTrue();
        }

        [Fact]
        public void ModelState_OnTooLongName_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeSellingPriceToClients_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = -1,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_SellingPriceToClientsIs0_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 0,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_OnNegativeSellingPriceToWholesalers_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = -1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_SellingPriceToWholesalersIs0_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 0
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_AlcoholContentIsNegative_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = -1,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }

        [Fact]
        public void ModelState_AlcoholContentIsLargerThan100_ReturnsFalse()
        {
            var sut = new ForCreationBeerDto()
            {
                Name = "Test",
                AlcoholContent = 1000,
                SellingPriceToClients = 1,
                SellingPriceToWholesalers = 1
            };
            // Set some properties here
            var context = new ValidationContext(sut, null, null);
            var results = new List<ValidationResult>();
            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(typeof(ForCreationBeerDto), typeof(ForCreationBeerDto)), typeof(ForCreationBeerDto));

            var isModelStateValid = Validator.TryValidateObject(sut, context, results, true);

            // Assert here
            isModelStateValid.Should().BeFalse();
        }
    }
}