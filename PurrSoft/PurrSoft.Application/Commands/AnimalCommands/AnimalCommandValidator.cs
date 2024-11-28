using FluentValidation;
using PurrSoft.Application.Commands.AnimalCommands;

namespace PurrSoft.Application.Commands.AuthCommands;

public class AnimalCreateCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public AnimalCreateCommandValidator()
    {
        RuleFor(e => e.animalDto.Name).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Gender).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Sterilized).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.ImageUrl).NotNull();
    }
}

public class AnimalUpdateCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public AnimalUpdateCommandValidator()
    {
        RuleFor(e => e.animalDto.Id).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Name).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Gender).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Sterilized).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.ImageUrl).NotNull();
    }
}

public class AnimalDeleteCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
    public AnimalDeleteCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
    }
}


