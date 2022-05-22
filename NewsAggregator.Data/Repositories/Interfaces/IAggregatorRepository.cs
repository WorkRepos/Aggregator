using NewsAggregator.Data.Entities;

namespace NewsAggregator.Data.Repositories.Interfaces
{
    public interface IAggregatorRepository
    {
        Task<RssUrl?> GetByUrlAsync(string url, CancellationToken cancellationToken);
        Task<IEnumerable<News>> GetByPageAsync(int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<News>> SearchAsync(string text, int skip, int take, CancellationToken cancellationToken);
    }
}
