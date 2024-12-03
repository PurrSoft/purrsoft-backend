using PurrSoft.Application.Common;
using PurrSoft.Application.Models;
using PurrSoft.Application.QueryOverviews;

namespace PurrSoft.Application.Queries.AnimalQueries;

public class GetFilteredAnimalsQueries : BaseRequest<CollectionResponse<AnimalOverview>>
{
    public int Skip { get; set; }
    public int Take { get; set; }
    public string? SortBy { get; set; }
    public string? SortOrder { get; set; }
    public string? Search { get; set; }
    public string? AnimalType { get; set; }
    public int? YearOfBirth { get; set; }
    public string? Gender { get; set; }
}

public class GetAnimalByIdQuery : BaseRequest<AnimalDto>
{
    public string? Id { get; set; }
}
