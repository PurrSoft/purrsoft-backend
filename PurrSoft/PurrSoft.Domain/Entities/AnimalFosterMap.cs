namespace PurrSoft.Domain.Entities;

public class AnimalFosterMap
{
	public Guid Id { get; set; }
	public DateTime StartFosteringDate { get; set; }
	public DateTime? EndFosteringDate { get; set; }
	public string? SupervisingComment { get; set; }

	public Guid AnimalId { get; set; }
	public Animal Animal { get; set; }

	public string FosterId { get; set; }
	public Foster Foster { get; set; }
}
