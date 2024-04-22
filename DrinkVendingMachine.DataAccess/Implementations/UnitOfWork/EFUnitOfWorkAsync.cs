using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.DataAccess.Implementations.UnitOfWork
{
    public class EFUnitOfWorkAsync : IUnitOfWorkAsync
    {
        private DrinkVendingMachineDBContext _dbContext;
        private bool _disposed;

        public EFUnitOfWorkAsync(DrinkVendingMachineDBContext dbContext)
            => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();

        public Task Rollback()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                { 
                    case EntityState.Added:
                        entry.State = EntityState.Detached; 
                        break;
                }
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!_disposed) 
            {
                if (disposing)
                    _dbContext.Dispose();
            }

            _disposed = true;
        }
    }
}
