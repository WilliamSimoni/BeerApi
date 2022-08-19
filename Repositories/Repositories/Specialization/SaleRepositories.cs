using Domain.Entities;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Base;

namespace Repositories.Repositories.Specialization
{
    public class SaleQueryRepository : QueryRepositoryBase<Sale>, ISaleQueryRepository
    {
        public SaleQueryRepository(AppDbContext context) : base(context)
        {
        }
    }

    public class SaleCommandRepository : CommandRepositoryBase<Sale>, ISaleCommandRepository
    {
        public SaleCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
