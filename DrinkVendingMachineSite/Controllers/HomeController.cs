using Azure;
using DrinkVendingMachine.API.Contracts;
using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.Services;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<DrinkEntity>> BuyDrinkAsync([FromBody] DrinksBuyResponse response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(response);
            }

            DrinkEntity drinkEntity = await _drinksService.GetByIdAsync(response.Id);

            if(drinkEntity == null)
                return NotFound($"There's not drink with Id: {response.Id}");

            drinkEntity.Count--;
            await _drinksService.Update(drinkEntity);


            foreach (var idCostPair in response.idCostPairs)
            {
                int coinId = idCostPair.Item1;
                int coinsCount = idCostPair.Item2;

                CoinEntity coinEntity = await _coinsService.GetByIdAsync(coinId);

                if(coinEntity == null)
                    return NotFound($"There's not coin with Id: {coinId}");

                coinEntity.Count += coinsCount;

                await _coinsService.Update(coinEntity);
            }

            await _drinksService.SaveChangesAsync();

            return Ok(drinkEntity);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CoinEntity>>> PickUpChange(IEnumerable<CoinEntity> coins) 
        {
            foreach (var coin in coins) 
            {
                CoinEntity coinEntityInDb = await _coinsService.GetByIdAsync(coin.Id);

                if(coinEntityInDb == null)
                    return NotFound($"There's not coin with Id: {coin.Id}");

                if (coinEntityInDb.Count < coin.Count)
                    return BadRequest("");

                coinEntityInDb.Count -= coin.Count;

                await _coinsService.Update(coinEntityInDb);
            }

            await _coinsService.SaveChangesAsync();

            return Ok(coins);
        }
    }
}
