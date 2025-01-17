namespace PurrSoft.Application.Models;

public class RequestDto
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public string RequestType { get; set; }
	public DateTime CreatedAt { get; set; }
	public string UserId { get; set; }
	public List<string>? Images { get; set; }

	// LeaveRequest specific properties
	public bool? Approved { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public TimeSpan? Duration { get; set; }

}
