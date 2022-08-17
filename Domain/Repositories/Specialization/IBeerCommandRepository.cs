using Domain.Entities;
using Domain.Repositories.Base;

namespace Domain.Repositories.Specialization
{
    public interface IBeerCommandRepository : ICommandRepositoryBase<Beer>
    {
    }
}
