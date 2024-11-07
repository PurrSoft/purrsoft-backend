using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.AnimalQueries;

public class GetAnimalsQuery : BaseRequest<CollectionResponse<AnimalDto>>
{
}

public class GetAnimalByIdQuery : BaseRequest<CommandResponse<AnimalDto>>
{
    public string Id { get; set; }
}
