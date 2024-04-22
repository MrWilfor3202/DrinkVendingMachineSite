namespace DrinkVendingMachine.API.Contracts
{
    public record DrinksBuyResponse(int Id, IEnumerable<(int, int)> idCostPairs);
}
