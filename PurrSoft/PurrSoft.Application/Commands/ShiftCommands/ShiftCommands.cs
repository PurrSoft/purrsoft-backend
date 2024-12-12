using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.ShiftCommands;

public class CreateShiftCommand : BaseRequest<CommandResponse>
{
	public ShiftDto ShiftDto { get; set; }
}

public class UpdateShiftCommand : BaseRequest<CommandResponse>
{
	public ShiftDto ShiftDto { get; set; }
}

public class DeleteShiftCommand : BaseRequest<CommandResponse>
{
	public string Id { get; set; }
}
