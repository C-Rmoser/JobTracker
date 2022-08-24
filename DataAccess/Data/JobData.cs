﻿using System.Data;
using DataAccess.DbAccess;
using DataAccess.Models;
using Serilog;

namespace DataAccess.Data;

public class JobData : IJobData
{
    private readonly ISqlDataAccess _db;

    public JobData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<JobModel>> GetJobs()
    {
        return _db.LoadData<JobModel, dynamic>("spJobs_GetAll", new { });
    }

    public async Task<JobModel?> GetJobById(int id)
    {
        var results = await _db.LoadData<JobModel, dynamic>(
            "spJobs_GetById", new {Id = id});

        return results.FirstOrDefault();
    }

    public async Task<int> InsertJob(JobModel job)
    {
        Log.Debug("Inserting {@job} into database", job);

        return await _db.SaveData("spJobs_Insert",
            new
            {
                job.Title, job.Location, job.Company, job.LinkToDetails,
                job.Origin, job.FirstSeenOn
            });
    }

    public async Task<int> InsertJobs(List<JobModel> jobs)
    {
        List<JobModel> dbJobs = (await GetJobs()).ToList();

        // Archive all jobs from same origin as parameter jobs.
        // Jobs that are still active are unarchived later on.
        foreach (var job in dbJobs)
        {
            if (job.Origin == jobs[0].Origin)
            {
                job.IsArchived = true;
            }
        }

        Log.Debug("Bulk inserting {@jobs} into database", jobs);

        var dt = new DataTable();
        dt.Columns.Add("Title");
        dt.Columns.Add("Location");
        dt.Columns.Add("LinkToDetails");
        dt.Columns.Add("Origin");
        dt.Columns.Add("FirstSeenOn");
        dt.Columns.Add("IsArchived");
        dt.Columns.Add("Company");

        foreach (var job in jobs)
        {
            var searchedJob = dbJobs.FirstOrDefault(dbJob => dbJob.LinkToDetails == job.LinkToDetails);

            if (searchedJob == null)
            {
                // New job postings will be inserted into the database.
                dt.Rows.Add(job.Title, job.Location, job.LinkToDetails, job.Origin,
                    job.FirstSeenOn, job.IsArchived, job.Company);
            }
            else
            {
                // Duplicate job means that it is already in the database. Undo archive from earlier.
                searchedJob.IsArchived = false;
            }
        }

        foreach (var job in dbJobs)
        {
            if (job.IsArchived)
            {
                // Update archived jobs in database.
                await UpdateJob(job);
            }
        }

        return await _db.SaveData("spJobs_BulkInsert", new {jobs = dt});
    }


    public async Task<int> ArchiveJob(int id)
    {
        return await _db.SaveData("spJobs_Archive", new {Id = id});
    }

    public async Task<int> UpdateJob(JobModel job) =>
        await _db.SaveData("spJobs_Update", job);
}