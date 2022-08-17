using System.Linq.Expressions;

namespace Domain.Repositories.Base
{
    /// <summary>
    /// Models a repository with basic query methods
    /// </summary>
    public interface IQueryRepositoryBase<T>
    {
        /// <summary>
        /// Returns all the entities contained in the repository
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Returns all the entities in the repository that satisfy the specified condition. 
        /// <example>
        /// For example, let us assume that our repository contains instances of a class with the attribute <c>id</c>.
        /// To retrieve the entity with <c>id = 1</c>, we could write:
        /// <code>
        /// int id = 1;
        /// var entityWithSpecificId = GetByCondition(x => x.id == id)
        /// </code>
        /// </example>
        /// </summary>
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition);

    }
}
