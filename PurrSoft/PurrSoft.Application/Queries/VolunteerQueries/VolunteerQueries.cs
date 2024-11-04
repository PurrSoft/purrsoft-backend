using AlbumStore.Application.QueryOverviews;
using PurrSoft.Application.Common;

namespace AlbumStore.Application.Queries.VolunteerQueries;

public class GetFilteredVolunteersQueries : BaseRequest<CollectionResponse<VolunteerOverview>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Search { get; set; }
    public string? Status { get; set; }
    public string? Tier { get; set; }
    public string? AssignedArea { get; set; }
}

public class GetVolunteerQuery : BaseRequest<VolunteerOverview>
{
    public string Id { get; set; }
}
