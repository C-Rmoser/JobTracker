namespace JobTrackerDataManager.DTOs;

public record DevJobsDto
{
    public string company { get; set; } = "";
    public string company_link { get; set; } = "";
    public string description { get; set; } = "";
    public string description_link { get; set; } = "";
    public string location { get; set; } = "";
    public string location_link { get; set; } = "";
    public string status { get; set; } = "";
    public string title { get; set; } = "";
    public int index { get; set; }
    public long timestamp { get; set; }
    public string timestampstring { get; set; } = "";
    public string uid { get; set; } = "";
    public string url { get; set; } = "";
    public string url_uid { get; set; } = "";
}