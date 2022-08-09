using JobTrackerDataManager.Models;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class ParseHubEndpoints
{
    public static void ConfigureParseHubEndpoints(this WebApplication app)
    {
        app.MapPost("/ParseHub", ProjectRunStatusChanged).AllowAnonymous();
    }

    internal static async Task<IResult> ProjectRunStatusChanged(ParseHubRunModel run)
    {
        Log.Information("Project run status changed: {@run}", run);
        return Results.Ok();
    }
}