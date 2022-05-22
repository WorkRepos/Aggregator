using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Contexts;
using NewsAggregator.Data.Entities;
using NewsAggregator.Data.Repositories.Interfaces;

namespace NewsAggregator.Data.Repositories.Classes
{
    public class AggregatorRepository : IAggregatorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AggregatorRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<RssUrl?> GetByUrlAsync(string url, CancellationToken cancellationToken)
        {
            return await _databaseContext.RssUrls.AsNoTracking().Where(x => x.Url == url).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<News>> GetByPageAsync(int skip, int take, CancellationToken cancellationToken)
        {
            return await _databaseContext.News.Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<News>> SearchAsync(string text, int skip, int take, CancellationToken cancellationToken)
        {
            return await _databaseContext.News.Include(x => x.RssUrl)
                .Where(x => x.Title.Contains(text))
                .OrderBy(x => x.Title)
                .Skip(skip).Take(take).ToListAsync(cancellationToken);
        }
    }
}
