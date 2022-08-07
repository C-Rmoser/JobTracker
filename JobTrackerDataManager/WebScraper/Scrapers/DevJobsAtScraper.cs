using DataAccess.Models;

namespace JobTrackerDataManager.WebScraper.Scrapers;

public class DevJobsAtScraper : IWebScraper
{
    public string ScraperType { get; set; }

    public List<JobModel> GetWebsiteData()
    {
        List<JobModel> jobs = new()
        {
            new JobModel()
            {
                Title = "C# Developer",
                Origin = "Dev Jobs"
            },
            new JobModel()
            {
                Title = "Full Stack Developer",
                Origin = "Dev Jobs"
            }
        };

        return jobs;
    }
}