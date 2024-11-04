namespace PurrSoft.Domain.Entities;

public class Volunteer
{
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public VolunteerStatus Status { get; set; }
    public TierLevel Tier { get; set; }
    public string AssignedArea { get; set; }

    public DateTime? LastShiftDate { get; set; }
    // public virtual ICollection<Shift> Shifts { get; set; }
    // public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public string? Bio { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Volunteer()
    {
        // Shifts = new List<Shift>();
        // LeaveRequests = new List<LeaveRequest>();
    }
}

public enum VolunteerStatus
{
    Active,
    OnLeave,
    Disabled
}

public enum TierLevel
{
    Trial,
    FullTime
}