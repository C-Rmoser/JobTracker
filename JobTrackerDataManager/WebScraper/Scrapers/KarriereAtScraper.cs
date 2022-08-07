using DataAccess.Models;
using JobTrackerDataManager.WebScraper.ScraperAPIWrapper;

namespace JobTrackerDataManager.WebScraper.Scrapers;

public class KarriereAtScraper : IWebScraper
{
    private readonly IHtmlWeb _web;
    public string ScraperType { get; set; } = "KarriereAt";

    private const string _url =
        @"https://www.karriere.at/jobs?keywords=c%23&locations=Salzburg%20(Stadt)&radius=30&focusResults=true";

    public KarriereAtScraper(IHtmlWeb web)
    {
        _web = web;
    }


    public List<JobModel> GetWebsiteData()
    {
        var htmlDoc = _web.Load(_url);

        // var jobNodes = htmlDoc.DocumentNode.SelectNodes("//li[contains(@class, 'm-jobsList__item')]");

        var titleNodes = htmlDoc.DocumentNode.SelectNodes("//a[contains(@class, 'm-jobsListItem__titleLink')]");

        List<JobModel> jobs = new List<JobModel>();

        foreach (var node in titleNodes)
        {
            jobs.Add(new JobModel()
            {
                Title = node.InnerText
            });
        }

        return jobs;
    }
}