using Domain.Entities;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using Moq;

namespace BeerApi.Test.Helpers.Mocks
{
    internal static class UnitOfWorkMock
    {
        public static Mock<IUnitOfWork> Get()
        {
            // Define query repository mocks
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var queryBreweryMock = QueryRepositoryMocks.GetBreweryQueryRep();
            var queryBeerMock = QueryRepositoryMocks.GetBeerQueryRep();
            var querySaleMock = QueryRepositoryMocks.GetSaleQueryRep();
            var queryWholesalerMock = QueryRepositoryMocks.GetWholesalerQueryRep();
            var queryInventoryBeer = QueryRepositoryMocks.GetInventoryBeerQueryRep();

            // Define command repository mocks
            var commandBeerMock = new Mock<IBeerCommandRepository>();
            var commandSaleMock = new Mock<ISaleCommandRepository>();
            var commandWholesalerMock = new Mock<IWholesalerCommandRepository>();
            var commandInventoryBeer = new Mock<IInventoryBeerCommandRepository>();

            // Plug query repository mocks into UnitOfWork mock
            unitOfWorkMock.Setup(u => u.QueryBrewery).Returns(queryBreweryMock.Object);
            unitOfWorkMock.Setup(u => u.QueryBeer).Returns(queryBeerMock.Object);
            unitOfWorkMock.Setup(u => u.QuerySale).Returns(querySaleMock.Object);
            unitOfWorkMock.Setup(u => u.QueryWholesaler).Returns(queryWholesalerMock.Object);
            unitOfWorkMock.Setup(u => u.QueryInventoryBeer).Returns(queryInventoryBeer.Object);

            // Plug command repository mocks into UnitOfWork mock
            unitOfWorkMock.Setup(u => u.ChangeBeer).Returns(commandBeerMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeSale).Returns(commandSaleMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeWholesaler).Returns(commandWholesalerMock.Object);
            unitOfWorkMock.Setup(u => u.ChangeInventoryBeer).Returns(commandInventoryBeer.Object);

            // Define saleCommandMock behavior
            commandSaleMock.Setup(q => q.Add(It.IsAny<Sale>()))
                .Callback((Sale entity) => entity.SaleId = 1);

            return unitOfWorkMock;
        }

    }
}
