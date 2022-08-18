using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;

namespace Repositories.Repositories.Base
{
    public class CommandRepositoryBase<T> : ICommandRepositoryBase<T> where T : class
    {

        private readonly DbSet<T> _entities;

        public CommandRepositoryBase(AppDbContext context)
        {
            _entities = context.Set<T>();
        }

        public void Add(T entity) => _entities.Add(entity);
        
        public void Remove(T entity) => _entities.Remove(entity);

        public void Update(T entity) => _entities.Update(entity);
    }
}
