using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Specialization
{
    public interface IWholesalerQueryRepository : IQueryRepositoryBase<Wholesaler>
    {
    }

    public interface IWholesalerCommandRepository : ICommandRepositoryBase<Wholesaler>
    {
    }
}