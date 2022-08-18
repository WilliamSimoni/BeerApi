using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using System.Linq.Expressions;

namespace Repositories.Repositories.Base
{
    public abstract class QueryRepositoryBase<T> : IQueryRepositoryBase<T> where T : class
    {

        private readonly DbSet<T> _entities;

        public QueryRepositoryBase(AppDbContext context)
        {
            _entities = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return await _entities.Where(condition).ToListAsync();
        }
    }
}
