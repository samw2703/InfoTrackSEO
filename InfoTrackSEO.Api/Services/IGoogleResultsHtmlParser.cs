using System.Text.RegularExpressions;

namespace InfoTrackSEO.Api.Services;

public interface IGoogleResultsHtmlParser
{
    List<string> GetUrls(string html);
}

public class GoogleResultsHtmlParser : IGoogleResultsHtmlParser
{
    public List<string> GetUrls(string html)
    {
        const string matchPattern = "/url\\?q=([a-zA-Z0-9.-?=/]*)";
        return Regex
            .Matches(html, matchPattern)
            .Select(x => x.Groups[1].Value)
            .ToList();
    }
}