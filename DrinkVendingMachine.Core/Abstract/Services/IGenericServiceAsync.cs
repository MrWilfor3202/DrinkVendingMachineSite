using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Core.Abstract.Services
{
    public interface IGenericServiceAsync<T>
    {
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<int> SaveChangesAsync();

        Task RollbackChanges();
    }
}
