namespace DrinkVendingMachine.Core.Abstract.UnitOfWork
{
    public interface IUnitOfWorkAsync : IDisposable
    {
        Task<int> CommitAsync();

        Task Rollback();
    }
}
