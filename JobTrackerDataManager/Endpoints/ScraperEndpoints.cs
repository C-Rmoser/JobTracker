using DataAccess.Data;
using DataAccess.Models;
using JobTrackerDataManager.DTOs;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class ScraperEndpoints
{
    public static void ConfigureSimpleScraperEndpoints(this WebApplication app)
    {
        app.MapPost("/karriere-at-web-hook", KarriereAtWebHook)
            .AllowAnonymous();
        app.MapPost("/dev-jobs-at-web-hook", DevJobsAtWebHook)
            .AllowAnonymous();
        app.MapPost("/jobs-at-web-hook", JobsAtWebHook)
            .AllowAnonymous();
    }

    internal static async Task<IResult> KarriereAtWebHook(List<KarriereAtDto> dtos, IJobData data)
    {
        Log.Information("Receiving Web Scraping data from karriere.at {@dtos}", dtos);

        if (dtos.Count == 0)
        {
            Log.Information("Received invalid arguments in KarriereWebHook");

            return Results.BadRequest("Invalid Arguments given.");
        }

        try
        {
            List<JobModel> jobs = Utilities.Utilities.MapKarriereAtDtosToJobs(dtos);
            await data.InsertJobs(jobs);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unable to Insert {@dtos} into database", dtos);
            return Results.Problem(ex.Message, null, 500);
        }

        return Results.Ok();
    }

    internal static async Task<IResult> DevJobsAtWebHook(List<DevJobsDto> dtos, IJobData data)
    {
        Log.Information("Receiving Web Scraping data from devjobs.at {@dtos}", dtos);

        if (dtos.Count == 0)
        {
            Log.Information("Received invalid arguments in DevJobsWebHook");

            return Results.BadRequest("Invalid Arguments given.");
        }

        try
        {
            List<JobModel> jobs = Utilities.Utilities.MapDevJobsAtDtosToJobs(dtos);
            await data.InsertJobs(jobs);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unable to Insert {@dtos} into database", dtos);
            return Results.Problem(ex.Message, null, 500);
        }

        return Results.Ok();
    }

    internal static async Task<IResult> JobsAtWebHook(List<JobsAtDto> dtos, IJobData data)
    {
        if (dtos.Count == 0)
        {
            Log.Information("Received invalid arguments in JobsAtWebHook.");

            return Results.BadRequest("Invalid Arguments given.");
        }

        Log.Information("Receiving Web Scraping data from jobs.at {@dtos}", dtos);
        try
        {
            List<JobModel> jobs = Utilities.Utilities.MapJobsAtDtosToJobs(dtos);
            await data.InsertJobs(jobs);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unable to Insert {@dtos} into database", dtos);
            return Results.Problem(ex.Message, null, 500);
        }

        return Results.Ok();
    }
}