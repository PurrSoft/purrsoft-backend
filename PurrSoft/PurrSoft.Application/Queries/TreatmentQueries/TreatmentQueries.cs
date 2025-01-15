using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Queries.TreatmentQueries
{
    // Query to retrieve a collection of treatments
    public class GetTreatmentsQuery : BaseRequest<CollectionResponse<TreatmentDto>>
    {
        // Optional filters based on relevant fields
        public Guid? IdAnimal { get; set; } // Filter by the associated animal ID
        public string? MedicationName { get; set; } // Filter by medication name
        public DateTime? TreatmentStart { get; set; } // Filter by the start date of treatment
        public DateTime? TreatmentEnd { get; set; } // Filter by the end date of treatment
    }

    // Query to retrieve a single treatment by its ID
    public class GetTreatmentByIdQuery : BaseRequest<CommandResponse<TreatmentDto>>
    {
        public required Guid Id { get; set; } // The ID of the treatment to retrieve
    }

    // Query to search for treatments by medication name or animal ID
    public class SearchTreatmentsQuery : BaseRequest<CollectionResponse<TreatmentDto>>
    {
        public string? MedicationName { get; set; } // Search by medication name
        public Guid? IdAnimal { get; set; } // Search by associated animal ID
    }

    // Query to retrieve treatments within a specific date range
    public class GetTreatmentsByDateRangeQuery : BaseRequest<CollectionResponse<TreatmentDto>>
    {
        public required DateTime StartDate { get; set; } // Start date for the range
        public required DateTime EndDate { get; set; } // End date for the range
    }
}