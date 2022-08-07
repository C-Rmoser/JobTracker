using DataAccess.Models;

namespace JobTrackerDataManager.WebScraper.Scrapers;

public interface IWebScraper
{
    public string ScraperType { get; set; }
    public List<JobModel> GetWebsiteData();
}