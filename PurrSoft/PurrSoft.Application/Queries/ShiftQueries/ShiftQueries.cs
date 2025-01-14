using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.QueryResponses;

namespace PurrSoft.Application.Queries.ShiftQueries;

public class GetFilteredShiftsQueries : BaseRequest<CollectionResponse<ShiftOverview>>
{
	public int Skip { get; set; }
	public int Take { get; set; }
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
	public DateTime? Start { get; set; }
	public string? ShiftType { get; set; }
	public string? VolunteerId { get; set; }
}

public class GetShiftQuery : BaseRequest<ShiftDto?>
{
	public Guid Id { get; set; }
}


public class GetShiftCountQuery : BaseRequest<ShiftCountByDateResponse>
{
	public DateTime Date { get; set; }
}

public class GetShiftVolunteersQuery: BaseRequest<CollectionResponse<ShiftVolunteerDto>>
{
	public required DateTime DayOfShift { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
}