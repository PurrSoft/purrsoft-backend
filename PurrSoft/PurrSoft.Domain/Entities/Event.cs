namespace PurrSoft.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Subtitle { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<EventVolunteerMap>? EventVolunteerMappings { get; set; }
}
