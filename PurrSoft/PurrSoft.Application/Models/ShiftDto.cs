namespace PurrSoft.Application.Models;

public class ShiftDto
{
	public Guid Id { get; set; }
	public DateTime Start { get; set; }
	public string ShiftType { get; set; }
	public string? VolunteerId { get; set; }
}
