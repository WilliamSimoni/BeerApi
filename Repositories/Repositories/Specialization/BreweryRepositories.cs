using Domain.Entities;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Base;

namespace Repositories.Repositories.Specialization
{
    public class BreweryQueryRepository : QueryRepositoryBase<Brewery>, IBreweryQueryRepository
    {
        public BreweryQueryRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class BreweryCommandRepository : CommandRepositoryBase<Brewery>, IBreweryCommandRepository
    {
        public BreweryCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
