using AlbumStore.Application.Models;
using PurrSoft.Application.Common;

namespace AlbumStore.Application.Commands.VolunteerCommands;

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
