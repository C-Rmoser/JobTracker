using JobTrackerDataManager.Models;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class ParseHubEndpoints
{
    public static void ConfigureParseHubEndpoints(this WebApplication app)
    {
        app.MapPost("/ParseHub", (ParseHubRunModel? run) =>
        {
            Log.Information("Received model: {@run}", run);
            return Results.Ok(run);
        }).AllowAnonymous();
    }
}