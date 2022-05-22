using Microsoft.EntityFrameworkCore;
using NewsAggregator.Data.Contexts;
using NewsAggregator.Data.Entities;
using System.Xml;

namespace NewsAggregator.Web.HostedServices
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TimedHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            var cancellationToken = (CancellationToken)state;

            using(var scope = _serviceScopeFactory.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetService<DatabaseContext>();
                var links = databaseContext.RssUrls.ToList();

                for (int i = 0; i < links.Count(); ++i)
                {
                    var link = links[i];
                    var allNewsLinks = databaseContext.News.Include(x => x.RssUrl)
                        .Where(x => x.RssUrlId == link.Id)
                        .Select(x => x.Link)
                        .ToHashSet();

                    var rssString = new HttpClient().GetStringAsync(link.Url, cancellationToken).Result;
                    var xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(rssString);
                    var xmlElement = xmlDocument.DocumentElement;

                    if (xmlElement == null || xmlElement != null && xmlElement.Name.ToLower() != "rss")
                        continue;

                    var childNodes = xmlElement.SelectNodes("//channel/item");
                    var entities = new List<News>();

                    foreach (var item in childNodes)
                    {
                        var xmlNode = (XmlNode)item;
                        var itemLink = xmlNode?["link"]?.InnerText;

                        if (allNewsLinks.Contains(itemLink))
                            continue;

                        entities.Add(new News()
                        {
                            Title = xmlNode?["title"]?.InnerText,
                            Link = xmlNode?["link"]?.InnerText,
                            RssUrlId = link.Id,
                            Description = xmlNode?["description"]?.InnerText,
                            Category = xmlNode?["category"]?.InnerText,
                            PublicationDate = DateTime.Parse(xmlNode?["pubDate"]?.InnerText)
                    });
                    }

                    databaseContext.News.AddRange(entities);
                    databaseContext.SaveChanges();
                }
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private News? GetNewsByLink(DatabaseContext databaseContext, string link)
        {
            return databaseContext.News.FirstOrDefault(x => x.Link == link);
        }
    }
}
