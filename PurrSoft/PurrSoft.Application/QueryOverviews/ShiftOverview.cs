namespace PurrSoft.Application.QueryOverviews;

public class ShiftOverview
{
	public Guid Id { get; set; }
	public DateTime Start { get; set; }
	public string ShiftType { get; set; }
	public string? VolunteerId { get; set; }
}
