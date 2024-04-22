using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;

namespace DrinkVendingMachine.Services.Implementations.Services
{
    public class GenericServiceAsync<T> : IGenericServiceAsync<T> where T : class
    {
        private IGenericRepositoryAsync<T> _genericRepositoryAsync;
        private IUnitOfWorkAsync _unitOfWork;

        public GenericServiceAsync(IGenericRepositoryAsync<T> genericRepositoryAsync,
                              IUnitOfWorkAsync unitOfWorkAsync) 
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _unitOfWork = unitOfWorkAsync;
        } 

        public async Task<T> AddAsync(T entity) 
            => await _genericRepositoryAsync.AddAsync(entity);

        public async Task Delete(T entity) => await _genericRepositoryAsync.Delete(entity);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _genericRepositoryAsync.GetAllAsync();

        public Task<T> GetByIdAsync(int id) => _genericRepositoryAsync.GetByIdAsync(id);

        public async Task RollbackChanges() => await _unitOfWork.Rollback();

        public async Task<int> SaveChangesAsync() 
            => await _unitOfWork.CommitAsync();

        public async Task Update(T entity) => await _genericRepositoryAsync.Update(entity);
    }
}
