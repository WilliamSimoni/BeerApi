using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Specialization
{
    public interface IInventoryBeerQueryRepository : IQueryRepositoryBase<InventoryBeer>
    {
    }

    public interface IInventoryBeerCommandRepository : ICommandRepositoryBase<InventoryBeer>
    {
    }
}