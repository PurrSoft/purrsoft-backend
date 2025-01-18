namespace PurrSoft.Application.QueryOverviews;

public class EventOverview
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Date { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public ICollection<string>? AttendingVolunteers { get; set; }
}
