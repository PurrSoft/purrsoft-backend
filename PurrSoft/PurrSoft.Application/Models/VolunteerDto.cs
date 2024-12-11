namespace PurrSoft.Application.Models;

public class VolunteerDto
{
    public string UserId { get; set; }
    public string StartDate { get; set; }
    public string? EndDate { get; set; }
    public string Status { get; set; }
    public string Tier { get; set; }
    public string? LastShiftDate { get; set; }
    public string? CreatedAt { get; set; }
    public string? UpdatedAt { get; set; }
    public IList<string>? Tasks { get; set; }
    public string AvailableHours { get; set; }
    public string? TrainingStartDate { get; set; }
    public string? SupervisorId { get; set; }
    public IList<string>? TrainersId { get; set; }
    // public ICollection<Shift> Shifts { get; set; }
    // public ICollection<LeaveRequest> LeaveRequests { get; set; }
}