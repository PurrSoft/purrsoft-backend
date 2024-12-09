using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.AnimalFosterMapQueries;

public class GetAnimalFosterMapById : BaseRequest<AnimalFosterMapDto>
{
	public string Id { get; set; }
}

public class GetFilteredAnimalFosterMapsQueries : BaseRequest<CollectionResponse<AnimalFosterMapOverview>>
{
	public int Skip { get; set; }
	public int Take { get; set; }
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
	public string? AnimalId { get; set; }
	public string? FosterId { get; set; }
	public DateTime? StartFosteringDate { get; set; }
	public DateTime? EndFosteringDate { get; set; }
	public string? SupervisingComment { get; set; }
}