using InfoTrackSEO.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSEO.Api.Controllers;

[ApiController]
public class SEOController : ControllerBase
{
    private readonly ISEOResultsProvider _seoResultsProvider;

    public SEOController(ISEOResultsProvider seoResultsProvider)
    {
        _seoResultsProvider = seoResultsProvider;
    }

    [Route("Check")]
    [HttpGet]
    public async Task<IActionResult> Check(string url, string searchString)
    {
        var results = await _seoResultsProvider.Search(url, searchString);

        return Ok(results);
    }
}