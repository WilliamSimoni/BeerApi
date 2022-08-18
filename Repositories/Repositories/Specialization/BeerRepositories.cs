using Domain.Entities;
using Domain.Repositories.Base;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Base;

namespace Repositories.Repositories.Specialization
{
    public class BeerQueryRepository : QueryRepositoryBase<Beer>, IBeerQueryRepository
    {
        public BeerQueryRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class BeerCommandRepository : CommandRepositoryBase<Beer>, IBeerCommandRepository
    {
        public BeerCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
