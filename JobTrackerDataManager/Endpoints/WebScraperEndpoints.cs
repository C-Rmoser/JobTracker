using DataAccess.Models;
using JobTrackerDataManager.WebScraper;
using JobTrackerDataManager.WebScraper.Scrapers;

namespace JobTrackerDataManager.Endpoints;

public static class WebScraperEndpoints
{
    public static void ConfigureWebScraperEndpoints(this WebApplication app)
    {
        app.MapGet("/WebScraper/GatherJobData", GatherJobData)
            .Produces<IEnumerable<JobModel>>();
    }

    internal static async Task<IResult> GatherJobData(IScraperFactory scraperFactory)
    {
        var scrapers = scraperFactory.CreateAll();
        List<JobModel> jobs = new();

        foreach (IWebScraper scraper in scrapers)
        {
            jobs.AddRange(scraper.GetWebsiteData());
        }

        return Results.Ok(jobs);
    }
}