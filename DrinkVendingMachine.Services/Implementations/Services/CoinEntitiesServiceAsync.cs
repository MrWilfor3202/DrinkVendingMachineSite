using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.Core.Models;


namespace DrinkVendingMachine.Services.Implementations.Services
{
    public class CoinEntitiesServiceAsync : GenericServiceAsync<CoinEntity>, ICoinEntitiesServiceAsync
    {
        public CoinEntitiesServiceAsync(IGenericRepositoryAsync<CoinEntity> genericRepositoryAsync, 
            IUnitOfWorkAsync unitOfWorkAsync) : base(genericRepositoryAsync, unitOfWorkAsync)
        {
        }
    }
}
