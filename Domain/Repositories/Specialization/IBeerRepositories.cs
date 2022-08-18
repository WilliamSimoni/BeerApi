using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Specialization
{
    public interface IBeerQueryRepository : IQueryRepositoryBase<Beer>
    {
    }

    public interface IBeerCommandRepository : ICommandRepositoryBase<Beer>
    {
    }
}