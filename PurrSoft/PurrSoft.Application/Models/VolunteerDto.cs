namespace PurrSoft.Application.Models;

public class VolunteerDto
{
    public string UserId { get; set; }
    public string StartDate { get; set; }
    public string? EndDate { get; set; }
    public string Status { get; set; }
    public string Tier { get; set; }
    public string LastShiftDate { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public ICollection<string> Tasks { get; set; }
    // public ICollection<Shift> Shifts { get; set; }
    // public ICollection<LeaveRequest> LeaveRequests { get; set; }
}