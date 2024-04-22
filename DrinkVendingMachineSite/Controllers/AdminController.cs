using DrinkVendingMachine.API.Contracts;
using DrinkVendingMachine.Core.Abstract.Repositories;
using DrinkVendingMachine.Core.Abstract.UnitOfWork;
using DrinkVendingMachine.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DrinkVendingMachineSite.Controllers
{
    public class AdminController : Controller
    {
        private IDrinkEntitiesRepositoryAsync _drinksRepository;
        private ICoinEntitiesRepositoryAsync _coinsRepository;
        private IUnitOfWorkAsync _unitOfWork;

        public AdminController(IDrinkEntitiesRepositoryAsync drinksRepository,
            ICoinEntitiesRepositoryAsync coinsEntities,
            IUnitOfWorkAsync unitOfWork)
        {
            _drinksRepository = drinksRepository;
            _coinsRepository = coinsEntities;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<DrinkEntity>> GetAllDrinksAsync() => await _drinksRepository.GetAllAsync();

        [HttpPost]
        public async Task<ActionResult<DrinkEntity>> CreateDrinkAsync([FromBody] DrinksResponse response)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(response);
            }

            DrinkEntity drinkEntity = new DrinkEntity() 
            {
                Id = response.Id, Name = response.Name, PathToImage = response.PathToImage, Price = response.Price
            };

            await _drinksRepository.AddAsync(drinkEntity);

            await _unitOfWork.CommitAsync();

            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<DrinkEntity>> UpdateDrinkAsync([FromBody] DrinksResponse response) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(response);
            }

            DrinkEntity drinkEntity = await _drinksRepository.GetByIdAsync(response.Id);

            drinkEntity.Name = response.Name;
            drinkEntity.Price = response.Price;
            drinkEntity.PathToImage = response.PathToImage;

            await _drinksRepository.Update(drinkEntity);

            await _unitOfWork.CommitAsync();

            return Ok(drinkEntity);
        }

        [HttpDelete]
        public async Task<ActionResult<DrinkEntity>> DeleteDrinkAsync(DrinkEntity entity) 
        {
            await _drinksRepository.Delete(entity);

            await _unitOfWork.CommitAsync();

            return Ok(entity);
        }

        [HttpGet]
        public async Task<IEnumerable<CoinEntity>> GetAllCoinsAsync() => await _coinsRepository.GetAllAsync();


        [HttpPut]
        public async Task<ActionResult<CoinEntity>> UpdateCoinAsync([FromBody] CoinsResponse response) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(response);
            }

            CoinEntity coinEntity = await _coinsRepository.GetByIdAsync(response.Id);

            coinEntity.Locked = response.Locked;
            coinEntity.Count = response.Count;

            await _coinsRepository.Update(coinEntity);

            await _unitOfWork.CommitAsync();

            return Ok(coinEntity);
        }

    }
}
