using MediatR;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Models;

public class ShiftDto
{
	public Guid Id { get; set; }
	public DateTime Start { get; set; }
	public string ShiftType { get; set; }
	public string ShiftStatus { get; set; }
	public string? VolunteerId { get; set; }

	public void Update(Shift shift)
	{
		shift.Start = Start;
		shift.ShiftType = Enum.Parse<ShiftType>(ShiftType);
		shift.ShiftStatus = Enum.Parse<ShiftStatus>(ShiftStatus);
		shift.VolunteerId = VolunteerId;
	}
}
