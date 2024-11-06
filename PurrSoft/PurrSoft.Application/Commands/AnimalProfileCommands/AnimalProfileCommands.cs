using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;

namespace PurrSoft.Application.Commands.AnimalProfileCommands;

public class AnimalProfileCommands
{
    public class AnimalProfileGetCommand : BaseRequest<CollectionResponse<AnimalProfile>>
    {
    }

    public class AnimalProfileCreateCommand : BaseRequest<CommandResponse<Guid>>
    {
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
    }

    public class AnimalProfileUpdateCommand : BaseRequest<CommandResponse>
    {
        public Guid Id { get; set; }
        public string? CurrentDisease { get; set; }
        public string? CurrentMedication { get; set; }
        public string? PastDisease { get; set; }
    }

    public class AnimalProfileDeleteCommand : BaseRequest<CommandResponse>
    {
        public Guid Id { get; set; }
    }
}