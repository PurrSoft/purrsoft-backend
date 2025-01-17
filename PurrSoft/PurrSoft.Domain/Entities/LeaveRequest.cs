namespace PurrSoft.Domain.Entities;

public class LeaveRequest : Request
{
    public bool Approved { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? Duration => EndDate > StartDate && (EndDate - StartDate).TotalDays < 1
        ? EndDate - StartDate
        : null;
}
