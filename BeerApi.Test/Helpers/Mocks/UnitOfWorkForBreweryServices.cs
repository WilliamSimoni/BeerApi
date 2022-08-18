using BeerApi.Test.Fixtures;
using BeerApi.Test.Systems.Services;
using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using Moq;
using Services.Abstract.UseCaseServices;
using System.Linq.Expressions;

namespace BeerApi.Test.Helpers.Mocks
{




    internal static class UnitOfWorkMockForBreweryServices
    {
        public static Mock<IUnitOfWork> GetMock()
        {
            var fakeBreweryData = TestQueryBreweryServiceFixtures.GetTestBreweries();
            var fakeBeerData = TestQueryBreweryServiceFixtures.GetTestBeers();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var queryBreweryMock = new Mock<IBreweryQueryRepository>();
            var queryBeerMock = new Mock<IBeerQueryRepository>();

            unitOfWorkMock.Setup(u => u.QueryBrewery).Returns(queryBreweryMock.Object);
            unitOfWorkMock.Setup(u => u.QueryBeer).Returns(queryBeerMock.Object);

            //Define queryBreweryMock behavior
            queryBreweryMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBreweryData);
            queryBreweryMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Brewery, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Brewery, Boolean>> condition) => fakeBreweryData.AsQueryable().Where(condition));

            //Define queryBeerMock behavior
            queryBeerMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBeerData);
            queryBeerMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Beer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Beer, Boolean>> condition) => fakeBeerData.AsQueryable().Where(condition));

            return unitOfWorkMock;
        }
    }
}
