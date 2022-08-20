using BeerApi.Controllers;
using BeerApi.Test.Fixtures;
using BeerApi.Test.Helpers.Mocks;
using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Entities;
using Domain.Logger;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Abstract;
using Services.Abstract.UseCaseServices;
using Services.UseCaseServices;
using System.Linq.Expressions;
using Xunit;

namespace BeerApi.Test.Systems.Services
{


    public class TestCommandBreweryController
    {
        private ILoggerManager loggerMock;

        public TestCommandBreweryController()
        {
            //Arrange for all tests
            loggerMock = new Mock<ILoggerManager>().Object;
        }

        [Fact]
        public async Task AddBeerToBrewery_OnSuccess_Returns201Created()
        {
            //Arrange
            var serviceMock = new Mock<IServicesWrapper>();
            var changeBreweryMock = new Mock<ICommandBreweryBeersServices>();

            serviceMock.Setup(s => s.ChangeBreweryBeers).Returns(changeBreweryMock.Object);

            changeBreweryMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new BeerDto());

            var controller = new BreweryCommandController(loggerMock, serviceMock.Object);
            
            //Action
            var result = await controller.AddBeerToBrewery(1, new ForCreationBeerDto());

            //Assert
            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBreweryDoesNotExist_ReturnsNotFoundErrorMessage()
        {
            //Arrange
            var serviceMock = new Mock<IServicesWrapper>();
            var changeBreweryMock = new Mock<ICommandBreweryBeersServices>();

            serviceMock.Setup(s => s.ChangeBreweryBeers).Returns(changeBreweryMock.Object);

            changeBreweryMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new BreweryNotFound(1));

            var controller = new BreweryCommandController(loggerMock, serviceMock.Object);

            //Action
            var result = await controller.AddBeerToBrewery(1, new ForCreationBeerDto());

            //Arrange
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task AddBeerToBrewery_OnBeerWithSameNameAlreadyExists_ReturnsConflictErrorMessage()
        {
            //Arrange
            var serviceMock = new Mock<IServicesWrapper>();
            var changeBreweryMock = new Mock<ICommandBreweryBeersServices>();

            serviceMock.Setup(s => s.ChangeBreweryBeers).Returns(changeBreweryMock.Object);

            changeBreweryMock.Setup(s => s.AddBeerToBrewery(It.IsAny<int>(), It.IsAny<ForCreationBeerDto>()))
                .ReturnsAsync(new BreweryBeerConflict("", 1));

            var controller = new BreweryCommandController(loggerMock, serviceMock.Object);

            //Action
            var result = await controller.AddBeerToBrewery(1, new ForCreationBeerDto());

            //Arrange
            result.Should().BeOfType<ObjectResult>();
            var objectResult = result as ObjectResult;
            objectResult.StatusCode.Should().Be(409);
        }
    }
}