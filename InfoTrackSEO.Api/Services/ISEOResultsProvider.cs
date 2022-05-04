namespace InfoTrackSEO.Api.Services
{
    public interface ISEOResultsProvider
    {
        Task<List<int>> Search(string url, string searchString);
    }

    public class SEOResultsProvider : ISEOResultsProvider
    {
        private readonly IGoogleResultsHtmlParser _htmlParser;
        private readonly IGoogleResultsHtmlProvider _htmlProvider;

        public SEOResultsProvider(IGoogleResultsHtmlParser htmlParser, IGoogleResultsHtmlProvider htmlProvider)
        {
            _htmlParser = htmlParser;
            _htmlProvider = htmlProvider;
        }

        public async Task<List<int>> Search(string url, string searchString)
        {
            var html = await _htmlProvider.GetResults(searchString);
            var googleResults = _htmlParser.GetUrls(html);

            return googleResults
                .Where(gr => UrlMatches(gr, url))
                .Select(x => googleResults.IndexOf(x))
                .Distinct()
                .ToList();
        }

        private bool UrlMatches(string googleResult, string url) => googleResult.ToLower().Contains(url.ToLower());
    }
}
