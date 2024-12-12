namespace PurrSoft.Application.Models;

public class UserRoleStatusDto
{
	public string Role { get; set; }
	public string? Status { get; set; }
}

public class UserRoleDatesDto
{
	public string Role { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? EndDate { get; set; }
}