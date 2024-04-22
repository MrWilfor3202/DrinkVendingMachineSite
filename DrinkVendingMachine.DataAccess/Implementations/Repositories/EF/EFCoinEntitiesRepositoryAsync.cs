using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Models;
using DrinkVendingMachine.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.DataAccess.Implementations.Repositories.EF
{
    public class EFCoinEntitiesRepositoryAsync : EFGenericRepositoryAsync<CoinEntity>, ICoinEntitiesRepositoryAsync
    {
        private DbSet<CoinEntity> _coinEntities;

        public EFCoinEntitiesRepositoryAsync(DrinkVendingMachineDBContext dbContext) : base(dbContext)
        {
            _coinEntities = dbContext.Set<CoinEntity>();
        }
    }
}
