using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.AccountCommands;

public class UpdateAccountCommand : BaseRequest<CommandResponse>
{
	public ApplicationUserDto ApplicationUserDto { get; set; }
}

public class UpdateUserRolesCommand : BaseRequest<CommandResponse>
{
	public string? Id { get; set; }
	public string[] Roles { get; set; }
}
