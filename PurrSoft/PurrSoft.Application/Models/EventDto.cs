namespace PurrSoft.Application.Models;

public class EventDto
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public DateTime? Date {  get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
    public ICollection<string>? Volunteers { get; set; }
}
