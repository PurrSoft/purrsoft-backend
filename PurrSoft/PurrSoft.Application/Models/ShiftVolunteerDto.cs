namespace PurrSoft.Application.Models;

public class ShiftVolunteerDto
{
    public required string ShiftId { get; set; }
    public required string VolunteerId { get; set; }
    public required string ShiftType { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
}
