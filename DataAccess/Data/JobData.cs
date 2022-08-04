using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class JobData : IJobData
{
    private readonly ISqlDataAccess _db;

    public JobData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<JobModel>> GetJobs() =>
        _db.LoadData<JobModel, dynamic>("spJobs_GetAll", new { });

    public async Task<JobModel?> GetJobById(int id)
    {
        var results = await _db.LoadData<JobModel, dynamic>(
            "spJobs_GetById", new {Id = id});

        return results.FirstOrDefault();
    }

    public Task InsertJob(JobModel job)
    {
        return _db.SaveData("spJobs_Insert",
            new
            {
                job.Title, job.Location, job.LinkToDetails,
                job.Origin, job.FirstSeenOn
            });
    }

    public Task ArchiveJob(int id) =>
        _db.SaveData("spJobs_Archive", new {Id = id});

    public Task UpdateJob(JobModel job) =>
        _db.SaveData("spJobs_Update", job);
}