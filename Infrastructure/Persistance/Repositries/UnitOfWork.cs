
namespace Persistance.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        private ConcurrentDictionary<string, object> _repo;
        public UnitOfWork(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
            _repo = new();
        }

        public IGenericRepo<TEntity, Tkey> GetGenericRepo<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
          => (IGenericRepo<TEntity, Tkey>)_repo.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepo<TEntity, Tkey>(_dbContext));

        // var typeName = typeof(TEntity).Name; //key
        // if (_repo.ContainsKey(typeName)) 
        //return (IGenericRepo < TEntity, Tkey>) _repo[typeName];
        // else
        // {
        //     var repo = new GenericRepo<TEntity, Tkey>(_dbContext);
        //     _repo.Add(typeName, repo);
        //     return repo;
        // }


        public async Task<int> SaveChangeAsync() => await _dbContext.SaveChangesAsync();

    }
}
