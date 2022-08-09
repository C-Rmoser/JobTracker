using System.Data;
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
        Log.Debug("Inserting {@job} into databaes", job);

        return await _db.SaveData("spJobs_Insert",
            new
            {
                job.Title, job.Location, job.Company, job.LinkToDetails,
                job.Origin, job.FirstSeenOn
            });
    }

    public async Task<int> InsertJobs(List<JobModel> jobs)
    {
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
            dt.Rows.Add(job.Title, job.Location, job.LinkToDetails, job.Origin,
                job.FirstSeenOn, job.IsArchived, job.Company);
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