namespace PurrSoft.Application.Models;

public class AnimalFosterMapDto
{
	public Guid? Id { get; set; }
	public Guid AnimalId { get; set; }
	public string FosterId { get; set; }
	public DateTime StartFosteringDate { get; set; }
	public DateTime? EndFosteringDate { get; set; }
	public string? SupervisingComment { get; set; }
}
