
namespace Persistance.Repositries
{
    public class GenericRepo<TEntity, Tkey> : IGenericRepo<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        private readonly ApplicationDBContext _dBContext;

        public GenericRepo(ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        
        public async Task AddAsync(TEntity entity) => await _dBContext.Set<TEntity>().AddAsync(entity);
        public void Delete(TEntity entity) => _dBContext.Set<TEntity>().Remove(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false)
  => asNoTracking ?
      await _dBContext.Set<TEntity>().AsNoTracking().ToListAsync() :
      await _dBContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity> GetByIdAsync(Tkey id) => await _dBContext.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> specifications)
           => await ApplySpecifications (specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(Specifications<TEntity> specifications)
         => await ApplySpecifications(specifications).FirstOrDefaultAsync();
        public async Task<int> CountAsync(Specifications<TEntity> specifications)
        => await ApplySpecifications(specifications).CountAsync();

        public void Update(TEntity entity) => _dBContext.Set<TEntity>().Update(entity);
       
        private IQueryable<TEntity> ApplySpecifications(Specifications<TEntity> specifications)
       => SpecificationEvalutor.GetQuery<TEntity>(_dBContext.Set<TEntity>(), specifications);

    
    }
}
