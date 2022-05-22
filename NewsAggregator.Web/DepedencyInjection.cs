using NewsAggregator.Data.Entities;
using NewsAggregator.Data.Repositories.Classes;
using NewsAggregator.Data.Repositories.Interfaces;
using NewsAggregator.Services.Classes;
using NewsAggregator.Services.Interfaces;

namespace NewsAggregator.Web
{
    public static class DepedencyInjection
    {
        public static void AddDI(this IServiceCollection services)
        {
            services.AddTransient<IAggregatorService, AggregatorService>();
            services.AddTransient<IAggregatorRepository, AggregatorRepository>();
            services.AddTransient<IGenericRepository<News>, GenericRepository<News>>();
            services.AddTransient<IGenericRepository<RssUrl>, GenericRepository<RssUrl>>();
        }
    }
}
