using DataAccess.Models;

namespace DataAccess.Data;

public interface IJobData
{
    Task<IEnumerable<JobModel>> GetJobs();
    Task<JobModel?> GetJobById(int id);
    Task InsertJob(JobModel job);
    Task ArchiveJob(int id);
    Task UpdateJob(JobModel job);
}