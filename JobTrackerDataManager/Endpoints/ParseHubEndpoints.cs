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

    internal static async Task<IResult> ParseHubWebHook(ParseHubRunDto? dto, IParseHubRunData data)
    {
        Log.Debug("Received model: {@run}", dto);

        try
        {
            ParseHubRunModel run = MapDto(dto!);

            int result = await data.InsertParseHubRun(run);

            if (result == 0)
            {
                return Results.Ok("Run could not be saved to database.");
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            Log.Warning("{@run} could not be saved to database", dto);
            return Results.Problem(ex.Message);
        }
    }

    // Maybe replace this with Autofaq at some point
    private static ParseHubRunModel MapDto(ParseHubRunDto dto)
    {
        return new ParseHubRunModel()
        {
            ProjectToken = dto.project_token,
            RunToken = dto.run_token,
            Status = dto.status,
            DataReady = dto.data_ready,
            StartTime = dto.start_time,
            EndTime = dto.end_time,
            Pages = dto.pages,
            Md5Sum = dto.md5sum,
            StartUrl = dto.start_url,
            StartTemplate = dto.start_template,
            StartValue = dto.start_value
        };
    }
}