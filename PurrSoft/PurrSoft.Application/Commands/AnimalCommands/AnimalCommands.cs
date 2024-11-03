using PurrSoft.Application.Common;
using PurrSoft.Domain.Entities;
using PurrSoft.Domain.Entities.Enums;

namespace PurrSoft.Application.Commands.AnimalCommands;

public class AnimalGetCommand: BaseRequest<CollectionResponse<Animal>>
{

}

public class AnimalCreateCommand: BaseRequest<CommandResponse<int>>
{
    public AnimalType animalType { get; set; }
    public string Name { get; set; }
    public int YearOfBirth { get; set; }
    public string Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public string ImageUrl { get; set; }
}

public class  AnimalUpdateCommand: BaseRequest<CommandResponse<Animal>>
{
    public Guid Id { get; set; }
    public AnimalType animalType { get; set; }
    public string Name { get; set; }
    public int YearOfBirth { get; set; }
    public string Gender { get; set; }
    public Boolean Sterilized { get; set; }
    public string ImageUrl { get; set; }
}

public class AnimalDeleteCommand: BaseRequest<CommandResponse<Animal>>
{
    public Guid Id { get; set; }
}