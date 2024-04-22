namespace DrinkVendingMachine.Core.Abstract.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}
