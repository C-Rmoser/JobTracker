using JobTrackerDataManager.DTOs;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class SimpleScraperEndpoints
{
    public static void ConfigureSimpleScraperEndpoints(this WebApplication app)
    {
        app.MapPost("/SimpleScraperWebHook", SimpleScraperWebHook)
            .AllowAnonymous();
    }

    internal static async Task<IResult> SimpleScraperWebHook(SimpleScraperDto? dto)
    {
        Log.Information("Receiving Web Scraping data. {@dto}", dto);
        return Results.Ok();
    }
}