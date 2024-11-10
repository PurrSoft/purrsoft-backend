using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.AnimalProfileQueries
{
    public class GetAnimalProfilesQuery : BaseRequest<CollectionResponse<AnimalProfileDto>>
    {
    }

    public class GetAnimalProfileByIdQuery : BaseRequest<CommandResponse<AnimalProfileDto>>
    {
        public string Id { get; set; }
    }
}