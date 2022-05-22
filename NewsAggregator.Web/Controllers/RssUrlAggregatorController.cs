using Microsoft.AspNetCore.Mvc;
using NewsAggregator.Services.Interfaces;

namespace NewsAggregator.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RssUrlAggregatorController : ControllerBase
    {
        private readonly IAggregatorService _aggregatorService;

        public RssUrlAggregatorController(IAggregatorService aggregatorService)
        {
            _aggregatorService = aggregatorService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string url, CancellationToken cancellationToken)
        {
            Uri? validatedUri;

            if (!Uri.TryCreate(url, UriKind.Absolute, out validatedUri))
                throw new ArgumentException("Url is not valid!");

            await _aggregatorService.AddUrlAsync(url, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _aggregatorService.DeleteUrlAsync(id, cancellationToken);

            return Ok();
        }
    }
}