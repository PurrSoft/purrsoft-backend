using FluentValidation;
using PurrSoft.Application.Commands.AnimalCommands;
using PurrSoft.Application.Helpers;

namespace PurrSoft.Application.Commands.AuthCommands;

public class AnimalCreateCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public AnimalCreateCommandValidator()
    {
        RuleFor(e => e.animalDto.Name).NotNull().NotEmpty();
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