using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Models;
using DrinkVendingMachine.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace DrinkVendingMachine.DataAccess.Implementations.Repositories.EF
{
    public class EFDrinkEntitiesRepositoryAsync : EFGenericRepositoryAsync<DrinkEntity>, IDrinkEntitiesRepositoryAsync
    {
        private readonly DbSet<DrinkEntity> _drinks;

        public EFDrinkEntitiesRepositoryAsync(DrinkVendingMachineDBContext dbContext) : base(dbContext)
            => _drinks = dbContext.Set<DrinkEntity>();
    }
}
