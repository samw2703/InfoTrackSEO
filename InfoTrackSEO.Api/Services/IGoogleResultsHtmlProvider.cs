using System.Net;

namespace InfoTrackSEO.Api.Services;

public interface IGoogleResultsHtmlProvider
{
    Task<string> GetResults(string searchString);
}

public class GoogleResultsHtmlProvider : IGoogleResultsHtmlProvider
{
    private WebClient _webClient;

    public GoogleResultsHtmlProvider(WebClient webClient)
    {
        _webClient = webClient;
    }

    public async Task<string> GetResults(string searchString)
    {
        var googleUrl = $"https://www.google.co.uk/search?num=100&q={string.Join('+', searchString.Split(" "))}";
        var responseStream = _webClient.OpenRead(googleUrl);
        return await new StreamReader(responseStream).ReadToEndAsync();
    }
}