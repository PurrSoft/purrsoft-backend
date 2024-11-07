using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class AnimalCreateCommand: BaseRequest<CommandResponse<string>>
{
    public AnimalType AnimalType { get; set; }
    public string? Name { get; set; }
    public int YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public string? ImageUrl { get; set; }
}

public class  AnimalUpdateCommand: BaseRequest<CommandResponse>
{
    public Guid Id { get; set; }
    public AnimalType AnimalType { get; set; }
    public string? Name { get; set; }
    public int YearOfBirth { get; set; }
    public string? Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public string? ImageUrl { get; set; }
}

public class AnimalDeleteCommand: BaseRequest<CommandResponse>
{
    public Guid Id { get; set; }
}