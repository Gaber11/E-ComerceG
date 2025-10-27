
namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
        IGenericRepo<TEntity, Tkey> GetGenericRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>;
    }
}
