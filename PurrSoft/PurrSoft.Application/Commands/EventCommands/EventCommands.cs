using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.EventCommands;

public class CreateEventCommand : BaseRequest<CommandResponse>
{
    public required EventDto EventDto { get; set; }
}

public class UpdateEventCommand : BaseRequest<CommandResponse>
{
    public required EventDto EventDto { get; set; }
}

public class DeleteEventCommand : BaseRequest<CommandResponse>
{
    public required string Id { get; set; }
}