using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.QueryOverviews;

public class FosterOverview
{
	public string UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime? EndDate { get; set; }
	public string Status { get; set; }
	public string Location { get; set; }
	public int? MaxAnimalsAllowed { get; set; }
	public string HomeDescription { get; set; }
	public string? ExperienceLevel { get; set; }
	public bool HasOtherAnimals { get; set; }
	public string? OtherAnimalDetails { get; set; }
	public int AnimalFosteredCount { get; set; }

	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	// public ICollection<Animal> FosteredAnimals { get; set; }
}
