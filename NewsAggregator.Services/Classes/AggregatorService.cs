using NewsAggregator.Data.Entities;
using NewsAggregator.Data.Repositories.Interfaces;
using NewsAggregator.Services.Interfaces;
using NewsAggregator.Shared.Models;

namespace NewsAggregator.Services.Classes
{
    public class AggregatorService : IAggregatorService
    {
        private readonly IAggregatorRepository _aggregatorRepository;
        private readonly IGenericRepository<News> _genericRepositoryNews;
        private readonly IGenericRepository<RssUrl> _genericRepositoryRssUrl;

        public AggregatorService(IAggregatorRepository aggregatorRepository,
            IGenericRepository<News> genericRepositoryNews,
            IGenericRepository<RssUrl> genericRepositoryRssUrl)
        {
            _aggregatorRepository = aggregatorRepository;
            _genericRepositoryNews = genericRepositoryNews;
            _genericRepositoryRssUrl = genericRepositoryRssUrl;
        }

        public async Task<IEnumerable<NewsModel>> GetByPageAsync(int skip, int take, CancellationToken cancellationToken)
        {
            var entities = await _aggregatorRepository.GetByPageAsync(skip, take, cancellationToken);
            var entityModels = new List<NewsModel>();

            foreach(var entity in entities)
            {
                entityModels.Add(new NewsModel()
                {
                    Title = entity.Title,
                    Link = entity.Link,
                    Description = entity.Description,
                    Category = entity.Category,
                    PublicationDate = entity.PublicationDate
                });
            }

            return entityModels;
        }

        public async Task AddUrlAsync(string url, CancellationToken cancellationToken)
        {
            await _genericRepositoryRssUrl.CreateAsync(new RssUrl() { Url = url }, cancellationToken);
        }

        public async Task DeleteUrlAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await _genericRepositoryRssUrl.FindByIdAsync(cancellationToken, id);
            await _genericRepositoryRssUrl.RemoveAsync(entity, cancellationToken);
        }

        public async Task<IEnumerable<NewsModel>> SearchAsync(string text, int skip, int take, CancellationToken cancellationToken)
        {
            var entities = await _aggregatorRepository.SearchAsync(text, skip, take, cancellationToken);
            var entityModels = new List<NewsModel>();

            foreach (var entity in entities)
            {
                entityModels.Add(new NewsModel()
                {
                    Title = entity.Title,
                    Link = entity.Link,
                    Description = entity.Description,
                    Category = entity.Category,
                    PublicationDate = entity.PublicationDate
                });
            }

            return entityModels;
        }

        public async Task<IEnumerable<UrlModel>> GetAllRssLinksAsync(CancellationToken cancellationToken)
        {
            var entities = await _genericRepositoryRssUrl.GetAsync(cancellationToken);
            var entityModels = new List<UrlModel>();

            foreach(var entity in entities)
            {
                entityModels.Add(new UrlModel()
                {
                    Id = entity.Id,
                    Url = entity.Url
                });
            }

            return entityModels;
        }
    }
}
