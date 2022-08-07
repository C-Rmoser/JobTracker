using DataAccess.Models;

namespace JobTrackerDataManager.WebScraper.Scrapers;

public class JobsAtScraper : IWebScraper
{
    public string ScraperType { get; set; }

    public List<JobModel> GetWebsiteData()
    {
        throw new NotImplementedException();
    }
}