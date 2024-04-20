using DrinkVendingMachineSite.DatabaseContext;
using DrinkVendingMachineSite.Models;
using DrinkVendingMachineSite.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace DrinkVendingMachineSite.Repositories.Implementation.EF
{
    public class EFDrinkEntityRepositoryAsync : EFGenericRepositoryAsync<DrinkEntity>, IDrinkEntityRepositoryAsync
    {
        private readonly DbSet<DrinkEntity> _drinks; 

        public EFDrinkEntityRepositoryAsync(DrinkVendingMachineDBContext dbContext) : base(dbContext)
            => _drinks = dbContext.Set<DrinkEntity>();
    }
}
