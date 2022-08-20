using Domain.Entities;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Base;

namespace Repositories.Repositories.Specialization
{
    public class InventoryBeerQueryRepository : QueryRepositoryBase<InventoryBeer>, IInventoryBeerQueryRepository
    {
        public InventoryBeerQueryRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class InventoryBeerCommandRepository : CommandRepositoryBase<InventoryBeer>, IInventoryBeerCommandRepository
    {
        public InventoryBeerCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
