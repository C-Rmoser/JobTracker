using DataAccess.Data;
using DataAccess.Models;
using Serilog;

namespace JobTrackerDataManager.Endpoints;

public static class JobEndpoints
{
    public static void ConfigureJobEndpoints(this WebApplication app)
    {
        app.MapGet("/Jobs", GetJobs)
            .Produces<IEnumerable<JobModel>>();
        app.MapGet("/Jobs/GetById/{id:int}", GetJobById)
            .Produces<JobModel>();
        app.MapPost("/Jobs", InsertJob);
        app.MapPost("/Jobs/BulkInsert", InsertJobs);
        app.MapPut("/Jobs/Archive/{id:int}", ArchiveJob);
        app.MapPut("/Jobs", UpdateJob);
    }

    internal static async Task<IResult> GetJobs(IJobData data)
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

    internal static async Task<IResult> GetJobById(int id, IJobData data)
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

    internal static async Task<IResult> InsertJob(JobModel job, IJobData data)
    {
        try
        {
            int result = await data.InsertJob(job);
            if (result == 0)
            {
                return Results.Ok("Job insertion failed.");
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    internal static async Task<IResult> InsertJobs(List<JobModel> jobs, IJobData data)
    {
        try
        {
            Log.ForContext("Jobs", jobs).Debug("Inserting jobs into database");

            int result = await data.InsertJobs(jobs);
            if (result == 0)
            {
                Log.Debug("Job insertion failed.");
                return Results.Ok("Job insertion failed.");
            }

            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    internal static async Task<IResult> ArchiveJob(int id, IJobData data)
    {
        try
        {
            int rowsAffected = await data.ArchiveJob(id);

            return rowsAffected > 0 ? Results.Ok() : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    internal static async Task<IResult> UpdateJob(JobModel job, IJobData data)
    {
        try
        {
            int rowsAffected = await data.UpdateJob(job);

            return rowsAffected > 0 ? Results.Ok() : Results.NotFound();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}