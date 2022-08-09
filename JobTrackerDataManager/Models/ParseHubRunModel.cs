namespace JobTrackerDataManager.Models;

public class ParseHubRunModel
{
    public string Name { get; set; }
    public string project_token { get; set; }
    public string run_token { get; set; }
    public string status { get; set; }
    public string data_ready { get; set; }
    public string start_time { get; set; }
    public string end_time { get; set; }
    public string pages { get; set; }
    public string md5sum { get; set; }
    public string start_url { get; set; }
    public string start_template { get; set; }
    public string start_value { get; set; }
}