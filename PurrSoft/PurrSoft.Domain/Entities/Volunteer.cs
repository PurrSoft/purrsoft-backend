using PurrSoft.Domain.Entities.Enum;

namespace PurrSoft.Domain.Entities;

public class Volunteer
{
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public VolunteerStatus Status { get; set; }
    public TierLevel Tier { get; set; }
    public DateTime? LastShiftDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IList<string>? Tasks { get; set; }
    public virtual ApplicationUser User { get; set; }
    // public virtual ICollection<Shift> Shifts { get; set; }
    // public virtual ICollection<LeaveRequest> LeaveRequests { get; set; }

    public Volunteer()
    {
        Tasks = new List<string>();
        // Shifts = new List<Shift>();
        // LeaveRequests = new List<LeaveRequest>();
    }
}