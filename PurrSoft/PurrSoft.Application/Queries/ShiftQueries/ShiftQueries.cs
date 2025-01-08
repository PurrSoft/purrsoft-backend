using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.ShiftQueries;

public class GetFilteredShiftsQueries : BaseRequest<CollectionResponse<ShiftOverview>>
{
	public int Skip { get; set; }
	public int Take { get; set; }
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
	public DateTime? UpperStartTime { get; set; }
	public DateTime? LowerStartTime { get; set; }
	public string? ShiftType { get; set; }
	public string? ShiftStatus { get; set; }
	public string? VolunteerId { get; set; }
}

public class GetShiftQuery : BaseRequest<ShiftDto?>
{
	public Guid Id { get; set; }
}
