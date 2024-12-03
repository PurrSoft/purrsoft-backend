using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.AnimalFosterMapCommands;

public class AnimalFosterMapCommands
{
	public class AddAnimalToFosterCommand : BaseRequest<CommandResponse>
	{
		public AnimalFosterMapDto AnimalFosterMapDto { get; set; }
	}

	public class RemoveAnimalFromFosterCommand : BaseRequest<CommandResponse>
	{
		public string AnimalFosterMapId { get; set; }
	}

	public class UpdateAnimalFosterMapCommand : BaseRequest<CommandResponse>
	{
		public AnimalFosterMapDto AnimalFosterMapDto { get; set; }
	}
}
