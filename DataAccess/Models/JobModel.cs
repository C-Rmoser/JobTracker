namespace DataAccess.Models;

public class JobModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public string Company { get; set; }
    public string LinkToDetails { get; set; }
    public string Origin { get; set; }
    public DateTime FirstSeenOn { get; set; }
    public bool IsArchived { get; set; }
}