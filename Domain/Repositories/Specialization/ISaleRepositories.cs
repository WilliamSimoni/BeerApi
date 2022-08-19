using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Specialization
{
    public interface ISaleQueryRepository : IQueryRepositoryBase<Sale>
    {
    }

    public interface ISaleCommandRepository : ICommandRepositoryBase<Sale>
    {
    }
}