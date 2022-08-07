using JobTrackerDataManager.WebScraper.Scrapers;

namespace JobTrackerDataManager.WebScraper;

public static class ScraperFactoryConfiguration
{
    public static void AddScraperFactory(this IServiceCollection services)
    {
        // Simply add additional Scrapers here.
        services.AddTransient<IWebScraper, KarriereAtScraper>();
        services.AddTransient<IWebScraper, DevJobsAtScraper>();

        services.AddSingleton<Func<IEnumerable<IWebScraper>>>
            (x => () => x.GetService<IEnumerable<IWebScraper>>()!);
        services.AddSingleton<IScraperFactory, ScraperFactory>();
    }
}