using Domain.Entities;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Base;

namespace Repositories.Repositories.Specialization
{
    public class WholesalerQueryRepository : QueryRepositoryBase<Wholesaler>, IWholesalerQueryRepository
    {
        public WholesalerQueryRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class WholesalerCommandRepository : CommandRepositoryBase<Wholesaler>, IWholesalerCommandRepository
    {
        public WholesalerCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}