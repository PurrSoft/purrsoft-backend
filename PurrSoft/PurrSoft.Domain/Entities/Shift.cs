using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Domain.Entities;

public class Shift
{
	public Guid Id { get; set; }
	public DateTime Start { get; set; }
	public ShiftType ShiftType { get; set; }
	public Volunteer? Volunteer { get; set; }
	public string? VolunteerId { get; set; }
}
