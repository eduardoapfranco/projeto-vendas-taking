namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
