using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Domain.Entities;

public class Animal
{
	public Guid Id { get; set; }
	public AnimalType AnimalType { get; set; }
	public string? Name { get; set; }
	public int YearOfBirth { get; set; }
	public string? Gender { get; set; }
	public bool Sterilized { get; set; }
	public string? ImageUrl { get; set; }
	public virtual IList<AnimalFosterMap> FosteredBy { get; set; }
	public Animal()
	{
		FosteredBy = new List<AnimalFosterMap>();
	}
}
