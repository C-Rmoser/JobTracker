using DataAccess.Models;

namespace DataAccess.Data;

public interface IParseHubRunData
{
    Task<IEnumerable<ParseHubRunModel>> GetParseHubRuns();
    Task<int> InsertParseHubRun(ParseHubRunModel run);
}