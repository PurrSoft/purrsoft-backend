using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.RequestCommands;

public class CreateRequestCommand : BaseRequest<CommandResponse>
{
	public RequestDto RequestDto { get; set; }
}

public class UpdateRequestCommand : BaseRequest<CommandResponse>
{
	public RequestDto RequestDto { get; set; }
}

public class DeleteRequestCommand : BaseRequest<CommandResponse>
{
	public string Id { get; set; }
}
