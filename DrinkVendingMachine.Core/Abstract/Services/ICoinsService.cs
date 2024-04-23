using DrinkVendingMachine.Core.Models;

namespace DrinkVendingMachine.Core.Abstract.Services
{
    public interface ICoinsService
    {
        IEnumerable<CoinEntity> PickupChange();

        int GetCurrentSum();

        void ContributeCoinToMachine(int coinId);
    }
}
