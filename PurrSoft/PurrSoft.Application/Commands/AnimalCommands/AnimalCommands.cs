using PurrSoft.Application.Common;
using PurrSoft.Application.Models;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class CreateAnimalCommand: BaseRequest<CommandResponse>
{
    public AnimalDto animalDto { get; set; }
}

public class  UpdateAnimalCommand: BaseRequest<CommandResponse>
{
    public AnimalDto animalDto { get; set; }
}

public class DeleteAnimalCommand: BaseRequest<CommandResponse>
{
    public string? Id { get; set; }
}