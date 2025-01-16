using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.AnimalProfileQueries
{
    public class GetAnimalProfilesQuery : BaseRequest<CollectionResponse<AnimalProfileDto>>
    {
        public string? Contract { get; set; }
        public string? ContractState { get; set; }
        public DateTime? ShelterCheckIn { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
        public string? Microchip { get; set; }
        public bool IncludeRabiesVaccine { get; set; }
        public bool IncludeMultivalentVaccine { get; set; }
    }

    public class GetAnimalProfileByIdQuery : BaseRequest<CommandResponse<AnimalProfileDto>>
    {
        public required string AnimalId { get; set; }
    }
}