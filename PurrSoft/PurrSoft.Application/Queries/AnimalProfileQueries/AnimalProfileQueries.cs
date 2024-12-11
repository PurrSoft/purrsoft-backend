using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.AnimalProfileQueries
{
    // Query to retrieve a collection of animal profiles
    public class GetAnimalProfilesQuery : BaseRequest<CollectionResponse<AnimalProfileDto>>
    {
        // Optional filters based on relevant fields
        public string? CurrentDisease { get; set; } // Filter by current medical condition
        public string? CurrentMedication { get; set; } // Filter by medication currently taken
        public string? PastDisease { get; set; } // Filter by past medical conditions
        public string? Passport { get; set; } // Filter by the animal's health or identification document
        public string? Microchip { get; set; } // Filter by the animal's unique microchip ID
    }

    // Query to retrieve a single animal profile by its ID
    public class GetAnimalProfileByIdQuery : BaseRequest<CommandResponse<AnimalProfileDto>>
    {


        public required string Id { get; set; } // The ID of the animal profile to retrieve
    }

    // Query to search for animal profiles using passport or microchip
    public class SearchAnimalProfileQuery : BaseRequest<CollectionResponse<AnimalProfileDto>>
    {
        public string? Passport { get; set; } // Search by the animal's health or identification document
        public string? Microchip { get; set; } // Search by the animal's unique microchip ID
    }

    // Query to retrieve animal profiles with missing or expired vaccinations
    public class GetAnimalProfilesWithMissingVaccinesQuery : BaseRequest<CollectionResponse<AnimalProfileDto>>
    {
        public bool IncludeRabiesVaccine { get; set; } // Include profiles with missing or expired rabies vaccine
        public bool IncludeMultivalentVaccine { get; set; } // Include profiles with missing or expired multivalent vaccine
    }
}