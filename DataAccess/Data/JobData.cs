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

    public async Task<int> InsertJob(JobModel job)
    {
        return await _db.SaveData("spJobs_Insert",
            new
            {
                job.Title, job.Location, job.LinkToDetails,
                job.Origin, job.FirstSeenOn
            });
    }

    public async Task<int> ArchiveJob(int id)
    {
        return await _db.SaveData("spJobs_Archive", new {Id = id});
    }

    public async Task<int> UpdateJob(JobModel job) =>
        await _db.SaveData("spJobs_Update", job);
}