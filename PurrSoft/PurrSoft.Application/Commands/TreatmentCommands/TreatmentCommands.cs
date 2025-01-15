using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Commands.TreatmentCommands
{
    public class TreatmentCommands
    {
        // Command to create a new Treatment
        public class TreatmentCreateCommand : BaseRequest<CommandResponse<Guid>>
        {
            public Guid IdAnimal { get; set; }
            public string? ImageUrl { get; set; }
            public required string MedicationName { get; set; }
            public required string MedicationTime { get; set; }
            public string? ExtraInfo { get; set; }
            public required DateTime TreatmentStart { get; set; }
            public required DateTime TreatmentEnd { get; set; }
            public required int TreatmentDays { get; set; }
        }

        // Command to update an existing Treatment
        public class TreatmentUpdateCommand : BaseRequest<CommandResponse>
        {
            public Guid Id { get; set; }
            public Guid IdAnimal { get; set; }
            public string? ImageUrl { get; set; }
            public required string MedicationName { get; set; }
            public required string MedicationTime { get; set; }
            public string? ExtraInfo { get; set; }
            public required DateTime TreatmentStart { get; set; }
            public required DateTime TreatmentEnd { get; set; }
            public required int TreatmentDays { get; set; }
        }

        // Command to delete an existing Treatment
        public class TreatmentDeleteCommand : BaseRequest<CommandResponse>
        {
            public Guid Id { get; set; }
        }
    }
}