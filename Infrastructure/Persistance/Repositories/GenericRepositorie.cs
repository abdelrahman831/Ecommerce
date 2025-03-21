

namespace Persistance.Repositories
{
    public class GenericRepositorie<TEntity, TKey> : IGenericRepositories<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepositorie(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity id) => _dbContext.Set<TEntity>().Remove(id); 

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking)=> asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() : await _dbContext.Set<TEntity>().ToListAsync();

        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
    }
}
