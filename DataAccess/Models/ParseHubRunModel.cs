namespace DataAccess.Models;

public class ParseHubRunModel
{
    public string? ProjectToken { get; set; }
    public string? RunToken { get; set; }
    public string? Status { get; set; }
    public string? DataReady { get; set; }
    public string? StartTime { get; set; }
    public string? EndTime { get; set; }
    public string? Pages { get; set; }
    public string? Md5Sum { get; set; }
    public string? StartUrl { get; set; }
    public string? StartTemplate { get; set; }
    public string? StartValue { get; set; }
}