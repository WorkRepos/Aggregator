using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Interfaces;
using NewsAggregator.Shared.Models;

namespace NewsAggregator.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsAggregatorController : ControllerBase
    {
        private readonly IAggregatorService _aggregatorService;

        public NewsAggregatorController(IAggregatorService aggregatorService)
        {
            _aggregatorService = aggregatorService;
        }

        [HttpGet("{skip}/{take}")]
        public async Task<IEnumerable<NewsModel>> Get(int skip, int take, CancellationToken cancellationToken)
        {
            return await _aggregatorService.GetByPageAsync(skip, take, cancellationToken).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task<IEnumerable<NewsModel>> Search(string searchText, int skip, int take, CancellationToken cancellationToken)
        {
            return await _aggregatorService.SearchAsync(searchText, skip, take, cancellationToken).ConfigureAwait(false);
        }
    }
}
