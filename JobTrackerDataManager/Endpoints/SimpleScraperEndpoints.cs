using JobTrackerDataManager.Models;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class SimpleScraperEndpoints
{
    public static void ConfigureSimpleScraperEndpoints(this WebApplication app)
    {
        app.MapPost("/SimpleScraperWebHook", SimpleScraperWebHook)
            .AllowAnonymous();
    }

    internal static async Task<IResult> SimpleScraperWebHook(List<SimpleScraperModel> data)
    {
        Log.Information("Receiving Web Scraping data. {@data}", data);
        return Results.Ok();
    }
}