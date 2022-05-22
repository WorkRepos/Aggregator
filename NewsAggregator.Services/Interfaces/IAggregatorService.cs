using NewsAggregator.Data.Entities;
using NewsAggregator.Shared.Models;

namespace NewsAggregator.Services.Interfaces
{
    public interface IAggregatorService
    {
        Task AddUrlAsync(string url, CancellationToken cancellationToken);
        Task<IEnumerable<NewsModel>> GetByPageAsync(int skip, int take, CancellationToken cancellationToken);
        Task DeleteUrlAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<NewsModel>> SearchAsync(string text, int skip, int take, CancellationToken cancellationToken);
        Task<IEnumerable<UrlModel>> GetAllRssLinksAsync(CancellationToken cancellationToken);
    }
}
