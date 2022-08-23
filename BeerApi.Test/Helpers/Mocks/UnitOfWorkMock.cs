using BeerApi.Test.Fixtures;
using BeerApi.Test.Systems.Services;
using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Base;
using Domain.Repositories.Specialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Moq;
using Services.Abstract.UseCaseServices;
using System.Linq.Expressions;

namespace BeerApi.Test.Helpers.Mocks
{
    internal static class UnitOfWorkMock
    {
        public static Mock<IUnitOfWork> Get()
        {
            var fakeBreweryData = BreweryFixtures.GetBreweries();
            var fakeBeerData = BeerFixtures.GetBeers();
            var fakeSaleData = SaleFixtures.GetSales();
            var fakeWholesalerData = WholesalerFixtures.GetWholesalers();
            var fakeInventoryBeerData = InventoryBeerFixtures.GetInventoryBeers();
            var fakeInventoryBeerDataWithInfo = InventoryBeerFixtures.GetInventoryBeersWithInfo();

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var queryBreweryMock = new Mock<IBreweryQueryRepository>();
            var queryBeerMock = new Mock<IBeerQueryRepository>();
            var querySaleMock = new Mock<ISaleQueryRepository>();
            var queryWholesalerMock = new Mock<IWholesalerQueryRepository>();
            var queryInventoryBeer = new Mock<IInventoryBeerQueryRepository>();

            var commandBeerMock = new Mock<IBeerCommandRepository>();
            var commandSaleMock = new Mock<ISaleCommandRepository>();
            var commandWholesalerMock = new Mock<IWholesalerCommandRepository>();
            var commandInventoryBeer = new Mock<IInventoryBeerCommandRepository>();

            unitOfWorkMock.Setup(u => u.QueryBrewery).Returns(queryBreweryMock.Object);
            unitOfWorkMock.Setup(u => u.QueryBeer).Returns(queryBeerMock.Object);
            unitOfWorkMock.Setup(u => u.QuerySale).Returns(querySaleMock.Object);
            unitOfWorkMock.Setup(u => u.QueryWholesaler).Returns(queryWholesalerMock.Object);
            unitOfWorkMock.Setup(u => u.QueryInventoryBeer).Returns(queryInventoryBeer.Object);

            unitOfWorkMock.Setup(u => u.ChangeBeer).Returns(commandBeerMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeSale).Returns(commandSaleMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeWholesaler).Returns(commandWholesalerMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeInventoryBeer).Returns(commandInventoryBeer.Object);

            //Define queryBreweryMock behavior
            queryBreweryMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBreweryData);
            queryBreweryMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Brewery, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Brewery, Boolean>> condition) => fakeBreweryData.AsQueryable().Where(condition));

            //Define queryBeerMock behavior
            queryBeerMock.Setup(q => q.GetAll()).ReturnsAsync(fakeBeerData);
            queryBeerMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Beer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Beer, Boolean>> condition) => fakeBeerData.AsQueryable().Where(condition));

            //Define saleQueryMock behavior
            querySaleMock.Setup(q => q.GetAll()).ReturnsAsync(fakeSaleData);
            querySaleMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Sale, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Sale, Boolean>> condition) => fakeSaleData.AsQueryable().Where(condition));

            //Define wholesalerQueryMock behavior
            queryWholesalerMock.Setup(q => q.GetAll()).ReturnsAsync(fakeWholesalerData);
            queryWholesalerMock.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<Wholesaler, Boolean>>>()))
                .ReturnsAsync((Expression<Func<Wholesaler, Boolean>> condition) => fakeWholesalerData.AsQueryable().Where(condition));

            //Define inventoryBeerQueryMock behavior
            queryInventoryBeer.Setup(q => q.GetAll()).ReturnsAsync(fakeInventoryBeerData);
            queryInventoryBeer.Setup(q => q.GetByCondition(It.IsAny<Expression<Func<InventoryBeer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<InventoryBeer, Boolean>> condition) => fakeInventoryBeerData.AsQueryable().Where(condition));
            queryInventoryBeer.Setup(q => q.GetBeerInfoByCondition(It.IsAny<Expression<Func<InventoryBeer, Boolean>>>()))
                .ReturnsAsync((Expression<Func<InventoryBeer, Boolean>> condition) => fakeInventoryBeerDataWithInfo.AsQueryable().Where(condition));

            //Define saleCommandMock behavior
            commandSaleMock.Setup(q => q.Add(It.IsAny<Sale>()))
                .Callback((Sale entity) => entity.SaleId = 1);

            return unitOfWorkMock;
        }
    }
}
