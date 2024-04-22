using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkVendingMachine.Services.Implementations.Services
{
    public class DrinkEntitiesServiceAsync : GenericServiceAsync<DrinkEntity>, IDrinkEntitiesServiceAsync
    {
        public DrinkEntitiesServiceAsync(IGenericRepositoryAsync<DrinkEntity> genericRepositoryAsync,
            IUnitOfWorkAsync unitOfWorkAsync) : base(genericRepositoryAsync, unitOfWorkAsync)
        {
        }
    }
}
