using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Commands.AnimalProfileCommands
{
    public class AnimalProfileCommands
    {
        // Command to create a new AnimalProfile
        public class AnimalProfileCreateCommand : BaseRequest<CommandResponse<Guid>>
        {
            public Guid AnimalId { get; set; }  // Animal ID to associate with this profile
            public string? CurrentDisease { get; set; }
            public string? CurrentMedication { get; set; }
            public string? PastDisease { get; set; }
        }

        // Command to update an existing AnimalProfile
        public class AnimalProfileUpdateCommand : BaseRequest<CommandResponse>
        {
            public Guid Id { get; set; }  // Profile ID to update
            public string? CurrentDisease { get; set; }
            public string? CurrentMedication { get; set; }
            public string? PastDisease { get; set; }
        }

        // Command to delete an existing AnimalProfile
        public class AnimalProfileDeleteCommand : BaseRequest<CommandResponse>
        {
            public Guid Id { get; set; }  // Profile ID to delete
        }
    }
}