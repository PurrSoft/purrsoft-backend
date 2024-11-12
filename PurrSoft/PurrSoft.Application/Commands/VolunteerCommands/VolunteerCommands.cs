using PurrSoft.Application.Models;
using PurrSoft.Application.Common;

namespace PurrSoft.Application.Commands.VolunteerCommands;

public class CreateVolunteerCommand : BaseRequest<CommandResponse>
{
    public VolunteerDto VolunteerDto { get; set; }
}

public class UpdateVolunteerCommand : BaseRequest<CommandResponse>
{
    public VolunteerDto VolunteerDto { get; set; }
}

public class DeleteVolunteerCommand : BaseRequest<CommandResponse>
{
    public string Id { get; set; }
}
