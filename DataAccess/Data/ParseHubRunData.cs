using DataAccess.DbAccess;
using DataAccess.Models;
using Serilog;

namespace DataAccess.Data;

public class ParseHubRunData : IParseHubRunData
{
    private readonly ISqlDataAccess _db;

    public ParseHubRunData(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task<IEnumerable<ParseHubRunModel>> GetParseHubRuns()
    {
        return _db.LoadData<ParseHubRunModel, dynamic>("spJobs_GetAll", new { });
    }

    public async Task<int> InsertParseHubRun(ParseHubRunModel run)
    {
        Log.Debug("Inserting {@job} into database", run);

        return await _db.SaveData("spParseHubRuns_Insert",
            new
            {
                run.ProjectToken, run.RunToken, run.Status, run.DataReady, run.StartTime,
                run.EndTime, run.Pages, run.Md5Sum, run.StartUrl, run.StartTemplate, run.StartValue
            });
    }
}