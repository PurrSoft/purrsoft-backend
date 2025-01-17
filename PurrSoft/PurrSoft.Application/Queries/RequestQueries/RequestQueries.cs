using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.RequestQueries;

public class GetFilteredRequestsQueries : BaseRequest<CollectionResponse<RequestOverview>>
{
	public int Skip { get; set; }
	public int Take { get; set; }
	public string? SortBy { get; set; }
	public string? SortOrder { get; set; }
	public DateTime? UpperCreatedAt { get; set; }
	public DateTime? LowerCreatedAt { get; set; }
	public string? RequestType { get; set; }
	public string? UserId { get; set; }


	public bool? Approved { get; set; }
	public DateTime? UpperStartDate { get; set; }
	public DateTime? LowerStartDate { get; set; }
	public DateTime? UpperEndDate { get; set; }
	public DateTime? LowerEndDate { get; set; }
	public TimeSpan? Duration { get; set; }
}

public class GetRequestQuery : BaseRequest<RequestDto?>
{
	public Guid Id { get; set; }
}