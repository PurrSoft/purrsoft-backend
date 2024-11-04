using FluentValidation;
using PurrSoft.Application.Commands.AnimalCommands;

namespace PurrSoft.Application.Commands.AuthCommands;

public class AnimalCreateCommandValidator : AbstractValidator<AnimalCreateCommand>
{
    public AnimalCreateCommandValidator()
    {
        RuleFor(e => e.Name).NotNull().NotEmpty();
        RuleFor(e => e.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.Gender).NotNull().NotEmpty();
        RuleFor(e => e.Sterilized).NotNull().NotEmpty();
        RuleFor(e => e.ImageUrl).NotNull();
    }
}

public class AnimalUpdateCommandValidator : AbstractValidator<AnimalUpdateCommand>
{
    public AnimalUpdateCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
        RuleFor(e => e.Name).NotNull().NotEmpty();
        RuleFor(e => e.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.Gender).NotNull().NotEmpty();
        RuleFor(e => e.Sterilized).NotNull().NotEmpty();
        RuleFor(e => e.ImageUrl).NotNull();
    }
}

public class AnimalDeleteCommandValidator : AbstractValidator<AnimalDeleteCommand>
{
    public AnimalDeleteCommandValidator()
    {
        RuleFor(e => e.Id).NotNull().NotEmpty();
    }
}


