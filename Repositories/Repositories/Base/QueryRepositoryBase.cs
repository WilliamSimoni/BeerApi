using Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using System.Linq.Expressions;

namespace Repositories.Repositories.Base
{
    public class QueryRepositoryBase<T> : IQueryRepositoryBase<T> where T : class
    {

        private readonly DbSet<T> _entities;

        public QueryRepositoryBase(AppDbContext context)
        {
            _entities = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entities;
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition)
        {
            return _entities.Where(condition);
        }
    }
}
