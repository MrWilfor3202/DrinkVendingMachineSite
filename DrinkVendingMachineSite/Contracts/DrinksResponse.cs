namespace DrinkVendingMachine.API.Contracts
{
    public record DrinksResponse(int Id, string Name, int Price, string PathToImage);
}
