namespace AlbumStore.Application.QueryOverviews;

public class VolunteerOverview
{
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; }
    public string Tier { get; set; }
    public string AssignedArea { get; set; }
    public DateTime? LastShiftDate { get; set; }
    // public ICollection<Shift> Shifts { get; set; }
    // public ICollection<LeaveRequest> LeaveRequests { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

