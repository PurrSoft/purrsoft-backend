namespace AlbumStore.Application.Models;

public class VolunteerDto
{
    public string UserId { get; set; }
    public string StartDate { get; set; }
    public string? EndDate { get; set; }
    public string Status { get; set; }
    public string Tier { get; set; }
    public string AssignedArea { get; set; }
    // public virtual ICollection<Shift> Shifts { get; set; }
    // public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
}