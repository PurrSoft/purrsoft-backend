using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrSoft.Application.Queries.FosterQueries
{
	public class GetFilteredFostersQueries : BaseRequest<CollectionResponse<FosterDto>>
	{
		public int Skip { get; set; }
		public int Take { get; set; }
		public string? SortBy { get; set; }
		public string? SortOrder { get; set; }
		public string? Search { get; set; }
		public string? Status { get; set; }
		public string? Location { get; set; }
		public string? ExperienceLevel { get; set; }
		public bool? HasOtherAnimals { get; set; }
	}

	public class GetFosterByIdQuery : BaseRequest<FosterDto>
	{
		public required string Id { get; set; }
	}
}
