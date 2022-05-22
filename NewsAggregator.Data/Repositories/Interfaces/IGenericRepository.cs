using System.Linq.Expressions;

namespace NewsAggregator.Data.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken);
        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        public Task<TEntity?> FindByIdAsync(CancellationToken cancellationToken, params object[] ids);
        public Task CreateAsync(TEntity item, CancellationToken cancellationToken);
        public Task UpdateAsync(TEntity item, CancellationToken cancellationToken);
        public Task RemoveAsync(TEntity item, CancellationToken cancellationToken);
    }
}
