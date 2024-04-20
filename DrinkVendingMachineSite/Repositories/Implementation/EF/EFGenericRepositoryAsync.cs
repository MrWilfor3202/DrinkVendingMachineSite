using DrinkVendingMachineSite.DatabaseContext;
using DrinkVendingMachineSite.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachineSite.Repositories.Implementation.EF
{
    public class EFGenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        private readonly DrinkVendingMachineDBContext _dbContext;

        public EFGenericRepositoryAsync(DrinkVendingMachineDBContext dbContext) 
            => _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() 
            => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
