using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using Domain.Repositories;
using FluentAssertions;
using Moq;
using Services.UseCaseServices;

namespace BeerApi.Test.Systems.Services
{

    public class TestSaleCommandServices
    {
        private SaleCommandServices service;

        private Mock<IUnitOfWork> unitOfWorkMock;
        public TestSaleCommandServices()
        {
            //Arrange for all tests
            var loggerMock = new Mock<ILoggerManager>();

            var mapper = MapperInstance.Get();

            unitOfWorkMock = UnitOfWorkMock.Get();

            service = new SaleCommandServices(loggerMock.Object, unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task AddSale_OnSuccess_ReturnsCreatedSaleDto()
        {
            //Action
            var serviceResult = await service.addSale(SaleFixtures.GetForCreationSaleDto().ElementAt(0));

            //Assert
            serviceResult.IsT1.Should().BeFalse();
            serviceResult.AsT0.Should().BeOfType<CreatedSaleDto>();
        }

        [Fact]
        public async Task AddSale_OnSuccess_ReturnsExpectedCreatedSaleDto()
        {
            //Action (test with the sale with the id 1 (which is at position 0 of the array))
            var serviceResult = await service
                .addSale(SaleFixtures.GetForCreationSaleDto().ElementAt(0));

            //Assert
            serviceResult.IsT1.Should().BeFalse();

            var expected = SaleFixtures.GetCreatedSaleDto().ElementAt(0);
            //The SaleDates can not be the same, so we:
            //1) test that the serviceResult date is newer than the expected date
            //2) set the SaleDate equals to the date in the serviceResult
            expected.SaleDate.Should().BeBefore(serviceResult.AsT0.SaleDate);
            expected.SaleDate = serviceResult.AsT0.SaleDate;

            //Also, the saleId is set automatically by the lower layer. So, we put it to the expected id. 
            //The current UnitOfWorkMock sets the entity SaleId always to 1
            //(the same id associated with the first element of the fixture)
            serviceResult.AsT0.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task AddSale_OnWholesalerNotFound_ReturnsWholesalerNotFoundError()
        {
            //Arrange
            var wrongForCreationSaleDto = SaleFixtures.GetForCreationSaleDto().ElementAt(0);
            wrongForCreationSaleDto.WholesalerId = 1000;

            //Action
            var serviceResult = await service.addSale(wrongForCreationSaleDto);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<WholesalerNotFound>();
        }

        [Fact]
        public async Task AddSale_OnBeerNotFound_ReturnsBeerNotFoundError()
        {
            //Arrange
            var wrongForCreationSaleDto = SaleFixtures.GetForCreationSaleDto().ElementAt(0);
            wrongForCreationSaleDto.BeerId = 1000;

            //Action
            var serviceResult = await service.addSale(wrongForCreationSaleDto);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<BeerNotFound>();
        }

        [Fact]
        public async Task AddSale_OnBeerNotInProduction_ReturnsBeerNotFoundError()
        {
            //Arrange
            var wrongForCreationSaleDto = SaleFixtures.GetForCreationSaleDto().ElementAt(0);
            //Beer 3 is not in production (InProduction = false)
            wrongForCreationSaleDto.BeerId = 3;

            //Action
            var serviceResult = await service.addSale(wrongForCreationSaleDto);

            //Assert
            serviceResult.IsT0.Should().BeFalse();
            serviceResult.AsT1.Should().BeOfType<BeerNotFound>();
        }

        [Fact]
        public async Task AddSale_OnSuccess_CallsSaveAsync()
        {
            //Action
            var serviceResult = await service.addSale(SaleFixtures.GetForCreationSaleDto().ElementAt(0));

            //Assert
            unitOfWorkMock.Verify(mock => mock.SaveAsync(), Times.Once());
        }

    }
}
