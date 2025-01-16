namespace PurrSoft.Domain.Entities;

public class EventVolunteerMap
{
    public Guid EventId { get; set; }
    public string VolunteerId { get; set; }
    public virtual Event? Event { get; set; }
    public virtual Volunteer? Volunteer { get; set; }
}
