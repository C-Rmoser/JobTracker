using DataAccess.Data;
using DataAccess.Models;
using JobTrackerDataManager.Models;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class ParseHubEndpoints
{
    public static void ConfigureParseHubEndpoints(this WebApplication app)
    {
        app.MapPost("/ParseHubWebHook", ParseHubWebHook)
            .AllowAnonymous();
    }

    internal static async Task<IResult> ParseHubWebHook(ParseHubRunDto? run, IParseHubRunData data)
    {
        Log.Information("Received model: {@run}", run);
        ParseHubRunModel runModel = new()
        {
            ProjectToken = run.project_token,
            RunToken = run.run_token,
            Status = run.status,
            DataReady = run.data_ready,
            StartTime = run.start_time,
            EndTime = run.end_time,
            Pages = run.pages,
            Md5Sum = run.md5sum,
            StartUrl = run.start_url,
            StartTemplate = run.start_template,
            StartValue = run.start_value
        };

        data.InsertParseHubRun(runModel);
        return Results.Ok(run);
    }
}