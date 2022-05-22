using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Contexts;
using NewsAggregator.Data.Repositories.Interfaces;
using System.Linq.Expressions;

namespace NewsAggregator.Data.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DatabaseContext _databaseContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken)
        {
            return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }

        public async Task<TEntity?> FindByIdAsync(CancellationToken cancellationToken, params object[] ids)
        {
            return await _dbSet.FindAsync(ids, cancellationToken);
        }

        public async Task CreateAsync(TEntity item, CancellationToken cancellationToken)
        {
            _dbSet.Add(item);
             await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        public async Task UpdateAsync(TEntity item, CancellationToken cancellationToken)
        {
            _databaseContext.Entry(item).State = EntityState.Modified;
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
        public async Task RemoveAsync(TEntity item, CancellationToken cancellationToken)
        {
            _dbSet.Remove(item);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}
