namespace Domain.Repositories.Base
{
    /// <summary>
    /// Models a repository with basic command methods
    /// </summary>
    public interface ICommandRepositoryBase<T>
    {
        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
