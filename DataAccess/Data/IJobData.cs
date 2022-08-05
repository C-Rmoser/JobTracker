using DataAccess.Models;

namespace DataAccess.Data;

public interface IJobData
{
    Task<IEnumerable<JobModel>> GetJobs();
    Task<JobModel?> GetJobById(int id);
    Task<int> InsertJob(JobModel job);
    Task<int> ArchiveJob(int id);
    Task<int> UpdateJob(JobModel job);
}