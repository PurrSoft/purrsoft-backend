using FluentValidation;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Helpers;

namespace PurrSoft.Application.Commands.AuthCommands;

public class AnimalCreateCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public AnimalCreateCommandValidator()
    {
        RuleFor(e => e.animalDto.Name).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Gender).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Sterilized).NotNull();
        RuleFor(e => e.animalDto.ImageUrl).NotNull();
    }
}

public class AnimalUpdateCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public AnimalUpdateCommandValidator()
    {
        RuleFor(e => e.animalDto.Id)
          .NotNull().NotEmpty()
          .Must(GuidValidationHelper.BeAValidGuid)
			    .WithMessage("Id must be a valid GUID.");;
        RuleFor(e => e.animalDto.Name).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.YearOfBirth).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.AnimalType).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Gender).NotNull().NotEmpty();
        RuleFor(e => e.animalDto.Sterilized).NotNull();
        RuleFor(e => e.animalDto.ImageUrl).NotNull();
    }
}

public class AnimalDeleteCommandValidator : AbstractValidator<DeleteAnimalCommand>
{
	public AnimalDeleteCommandValidator()
	{
		RuleFor(e => e.Id)
			.NotNull().NotEmpty()
			.Must(GuidValidationHelper.BeAValidGuid)
			.WithMessage("Id must be a valid GUID.");
	}
}