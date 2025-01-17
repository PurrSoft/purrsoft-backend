using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.EventQueries;

public class GetFilteredEventsQueries : BaseRequest<CollectionResponse<EventOverview>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Search { get; set; }
    public DateTime? Date { get; set; }
    public string? Location { get; set; }
    public string? AttendingVolunteer { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class GetEventByIdQuery : BaseRequest<EventDto>
{
    public string? Id { get; set; }
}