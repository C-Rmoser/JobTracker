using JobTrackerDataManager.WebScraper.Scrapers;

namespace JobTrackerDataManager.WebScraper;

public interface IScraperFactory
{
    IWebScraper Create(string location);
    IEnumerable<IWebScraper> CreateAll();
}