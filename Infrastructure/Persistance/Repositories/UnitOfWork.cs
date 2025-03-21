

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private ConcurrentDictionary<string, object> _repositories; 

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new ConcurrentDictionary<string, object>();
        }
        public IGenericRepositories<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
            => (IGenericRepositories<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepositorie<TEntity, TKey>(_dbContext));

        //var type = typeof(TEntity).Name;
        //if (!_repositories.ContainsKey(type))
        //{
        //    return (GenericRepositorie<TEntity, TKey>) _repositories[type];
        //}
        //else
        //{
        //    var repo = new GenericRepositorie<TEntity, TKey>(_dbContext);
        //    _repositories.Add(type, repo);
        //    return repo;
        //}



        public async Task<int> SaveChangesAsync()=>await _dbContext.SaveChangesAsync();
    }
}
