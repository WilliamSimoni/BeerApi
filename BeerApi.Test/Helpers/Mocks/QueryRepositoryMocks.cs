using BeerApi.Test.Fixtures;
using Domain.Entities;
using Domain.Repositories.Specialization;
using Moq;
using System.Linq.Expressions;

namespace BeerApi.Test.Helpers.Mocks
{
    internal static class QueryRepositoryMocks
    {
        public static Mock<IBreweryQueryRepository> GetBreweryQueryRep()
        {
            var queryBreweryMock = new Mock<IBreweryQueryRepository>();
            var fakeBreweryData = BreweryFixtures.GetBreweries();

            //Define queryBreweryMock behavior
            queryBreweryMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBreweryData);
            queryBreweryMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Brewery, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Brewery, Boolean>> condition) => fakeBreweryData.AsQueryable().Where(condition));
            return queryBreweryMock;
        }

        public static Mock<IBeerQueryRepository> GetBeerQueryRep()
        {
            var queryBeerMock = new Mock<IBeerQueryRepository>();
            var fakeBeerData = BeerFixtures.GetBeers();

            //Define queryBeerMock behavior
            queryBeerMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBeerData);
            queryBeerMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Beer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Beer, Boolean>> condition) => fakeBeerData.AsQueryable().Where(condition));
            return queryBeerMock;
        }

        public static Mock<ISaleQueryRepository> GetSaleQueryRep()
        {
            var querySaleMock = new Mock<ISaleQueryRepository>();
            var fakeSaleData = SaleFixtures.GetSales();

            //Define saleQueryMock behavior
            querySaleMock.Setup(q => q.GetAll()).ReturnsAsync(fakeSaleData);
            querySaleMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Sale, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Sale, Boolean>> condition) => fakeSaleData.AsQueryable().Where(condition));
            return querySaleMock;
        }

        public static Mock<IWholesalerQueryRepository> GetWholesalerQueryRep()
        {
            var queryWholesalerMock = new Mock<IWholesalerQueryRepository>();
            var fakeWholesalerData = WholesalerFixtures.GetWholesalers();

            //Define wholesalerQueryMock behavior
            queryWholesalerMock.Setup(q => q.GetAll()).ReturnsAsync(fakeWholesalerData);
            queryWholesalerMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Wholesaler, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Wholesaler, Boolean>> condition) => fakeWholesalerData.AsQueryable().Where(condition));
            return queryWholesalerMock;
        }

        public static Mock<IInventoryBeerQueryRepository> GetInventoryBeerQueryRep()
        {
            var queryInventoryBeer = new Mock<IInventoryBeerQueryRepository>();
            var fakeInventoryBeerData = InventoryBeerFixtures.GetInventoryBeers();
            var fakeInventoryBeerDataWithInfo = InventoryBeerFixtures.GetInventoryBeersWithInfo();

            //Define inventoryBeerQueryMock behavior
            queryInventoryBeer.Setup(q => q.GetAll()).ReturnsAsync(fakeInventoryBeerData);
            queryInventoryBeer.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<InventoryBeer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<InventoryBeer, Boolean>> condition) => fakeInventoryBeerData.AsQueryable().Where(condition));
            queryInventoryBeer.Setup(q => q.GetBeerInfoByCondition(It.IsAny<Expression<Func<InventoryBeer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<InventoryBeer, Boolean>> condition) => fakeInventoryBeerDataWithInfo.AsQueryable().Where(condition));
            return queryInventoryBeer;
        }
    }
}
