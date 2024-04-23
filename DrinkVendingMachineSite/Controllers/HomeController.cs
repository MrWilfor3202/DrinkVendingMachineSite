using DrinkVendingMachine.API.ExtensionsMethods;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace DrinkVendingMachineSite.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private IDrinkEntitiesServiceAsync _drinksService;
        private ICoinEntitiesServiceAsync _coinsService;

        public HomeController(IDrinkEntitiesServiceAsync drinksService,
            ICoinEntitiesServiceAsync coinsService) 
        {
            _drinksService = drinksService;
            _coinsService = coinsService;
        }

        [HttpGet]
        public async Task<IEnumerable<DrinkEntity>> GetAllDrinksAsync() => await _drinksService.GetAllAsync();

        [HttpPost]
        public async Task<ActionResult> ContributeCoin(int id)
        {
            Dictionary<int, int> coinsDict;

            if (await _coinsService.GetByIdAsync(id) == null)
                return NotFound($"There isn't coin with id {id}");

            if (!HttpContext.Session.Keys.Contains("contributedCoinsCollection"))
            {
                HttpContext.Session.SetCollectionAsJson("contributedCoinsCollection", 
                    new Dictionary<int, int> { [id] = 1 });

                return Ok();
            }

            coinsDict = HttpContext
                   .Session
                   .GetCollectionFromJson<Dictionary<int, int>>("contributedCoinsCollection");

            if (coinsDict.ContainsKey(id))
                coinsDict[id]++;
            else
                coinsDict.Add(id, 1);


            HttpContext.Session.SetCollectionAsJson("contributedCoinsCollection", coinsDict);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> BuyDrinkAsync(int id) 
        {
            var coinsDict = HttpContext
                 .Session
                 .GetCollectionFromJson<Dictionary<int, int>>("contributedCoinsCollection");

            DrinkEntity drinkEntity = await _drinksService.GetByIdAsync(id);

            if (drinkEntity == null)
                return NotFound($"There isn't drink with id {id}");

            if (drinkEntity.Count < 0)
                return BadRequest($"Drinks are out! Drink's id is {id}");

            int sum = 0;

            foreach (var coinIdCount in coinsDict)
            {
                var coinEntity = await _coinsService.GetByIdAsync(coinIdCount.Key);
                sum += coinEntity.Price * coinIdCount.Value;
            }

            if (sum < drinkEntity.Price)
                return BadRequest($"You need more money for this drink! Drink's id is {id}");

            foreach(var coinIdCount in coinsDict)
            {
                var coinEntity = await _coinsService.GetByIdAsync(coinIdCount.Key);
                coinEntity.Count -= coinIdCount.Value;
                await _coinsService.Update(coinEntity);
            }

            drinkEntity.Count--;

            await _drinksService.Update(drinkEntity);
            await _drinksService.SaveChangesAsync();    

            return Ok();
        }

        [HttpGet]
        public async Task<bool> CanGetFullChange(int sum)
        {
            var allCoins = await _coinsService.GetAllAsync();

            allCoins = allCoins.OrderByDescending(c => c.Price).ToImmutableList();

            foreach (var coin in allCoins)
            {
                if (sum == 0)
                    return true;

                int expectedCount = sum / coin.Price;
                int actualCount = Math.Min(coin.Price * expectedCount, coin.Price * coin.Count);

                sum -= actualCount;
            }

            return false;
        }

        private async Task<IEnumerable<CoinEntity>> CountTheChange(int sum)
        {
            var allCoins = await _coinsService.GetAllAsync();

            allCoins = allCoins.OrderByDescending(c => c.Price).ToImmutableList();

            IList<CoinEntity> result = new List<CoinEntity>();

            foreach (var coin in allCoins)
            {
                if (sum == 0)
                    break;
                    
                int expectedCount = sum / coin.Price;
                int actualCount = Math.Min(coin.Price * expectedCount, coin.Price * coin.Count);

                sum -= actualCount;

                CoinEntity coinEntity = new CoinEntity() { Id = coin.Id, Count = actualCount, Locked = false, Price = coin.Price };
                result.Add(coinEntity);
            }

            return result;

        }

    }
}
