using JobTrackerDataManager.WebScraper.Scrapers;

namespace JobTrackerDataManager.WebScraper;

public class ScraperFactory : IScraperFactory
{
    private readonly Func<IEnumerable<IWebScraper>> _factory;

    public ScraperFactory(Func<IEnumerable<IWebScraper>> factory)
    {
        _factory = factory;
    }

    public IWebScraper Create(string location)
    {
        var set = _factory();
        IWebScraper output = set.First(x => x.ScraperType == location);

        return output;
    }

    public IEnumerable<IWebScraper> CreateAll()
    {
        return _factory();
    }
}