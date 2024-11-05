using PurrSoft.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.Models
{
	public class FosterDto
	{
		public string UserId { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public FosterStatus Status { get; set; }
		public string Location { get; set; }
		public int? MaxAnimalsAllowed { get; set; }
		public string HomeDescription { get; set; }
		public string? ExperienceLevel { get; set; }
		public bool HasOtherAnimals { get; set; }
		public string? OtherAnimalDetails { get; set; }
		//public ICollection<AnimalDto> FosteredAnimals { get; set; }

		public FosterDto()
		{

		}
	}
}
