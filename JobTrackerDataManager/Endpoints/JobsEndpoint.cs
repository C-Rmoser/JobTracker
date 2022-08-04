using DataAccess.Data;
using DataAccess.Models;

namespace JobTrackerDataManager.Endpoints;

public static class JobsEndpoint
{
    public static void ConfigureJobsEndpoint(this WebApplication app)
    {
        app.MapGet("/Jobs", GetJobs);
        app.MapGet("/Jobs/GetById/{id:int}", GetJobById);
        app.MapPost("/Jobs/Insert", InsertJob);
        app.MapPut("/Jobs/Archive/{id:int}", ArchiveJob);
        app.MapPut("/Jobs", UpdateJob);
    }

    private static async Task<IResult> GetJobs(IJobData data)
    {
        try
        {
            return Results.Ok(await data.GetJobs());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetJobById(int id, IJobData data)
    {
        try
        {
            var results = await data.GetJobById(id);
            return results is null ? Results.NotFound() : Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> InsertJob(JobModel job, IJobData data)
    {
        try
        {
            await data.InsertJob(job);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> ArchiveJob(int id, IJobData data)
    {
        try
        {
            await data.ArchiveJob(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> UpdateJob(JobModel job, IJobData data)
    {
        try
        {
            await data.UpdateJob(job);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}