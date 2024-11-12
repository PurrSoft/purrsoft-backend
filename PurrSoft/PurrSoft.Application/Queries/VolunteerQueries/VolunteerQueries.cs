using PurrSoft.Application.QueryOverviews;
using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.VolunteerQueries;

public class GetFilteredVolunteersQueries : BaseRequest<CollectionResponse<VolunteerOverview>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Search { get; set; }
    public string? Status { get; set; }
    public string? Tier { get; set; }
}

public class GetVolunteerQuery : BaseRequest<VolunteerDto>
{
    public string Id { get; set; }
}
