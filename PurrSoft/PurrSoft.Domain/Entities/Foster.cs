using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Domain.Entities
{
	public class Foster
	{
		public string UserId { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public FosterStatus Status { get; set; }
		public string Location { get; set; }

		public int? MaxAnimalsAllowed { get; set; }
		public string HomeDescription { get; set; }
		public string? ExperienceLevel { get; set; }
		public bool HasOtherAnimals { get; set; }
		public string? OtherAnimalDetails { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		
		public virtual ApplicationUser User { get; set; }
		//public virtual ICollection<Animal> FosteredAnimals { get; set; }

		public Foster()
		{
			//FosteredAnimals = new List<Animal>();
		}
	}
}
