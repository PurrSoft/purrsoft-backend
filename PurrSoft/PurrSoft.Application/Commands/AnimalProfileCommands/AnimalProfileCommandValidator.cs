using FluentValidation;

namespace PurrSoft.Application.Commands.AnimalProfileCommands;

public class AnimalProfileCreateCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileCreateCommand>
{
    public AnimalProfileCreateCommandValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("AnimalId is required.");

        RuleFor(x => x.ContractState)
            .MaximumLength(10).WithMessage("Contract state should not be longer that 10 characters.");

        RuleFor(x => x.Microchip)
            .MaximumLength(100).WithMessage("Microchip should not exceed 100 characters.");

        RuleFor(x => x.CurrentDisease)
            .MaximumLength(500).WithMessage("CurrentDisease should not exceed 500 characters.");

        RuleFor(x => x.CurrentMedication)
            .MaximumLength(500).WithMessage("CurrentMedication should not exceed 500 characters.");

        RuleFor(x => x.PastDisease)
            .MaximumLength(500).WithMessage("PastDisease should not exceed 500 characters.");

        RuleFor(x => x.UsefulLinks)
            .Must(list => list.All(link => link.Length <= 200))
            .WithMessage("Each link in UsefulLinks must be 200 characters or less.");
    }
}

public class AnimalProfileUpdateCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileUpdateCommand>
{
    public AnimalProfileUpdateCommandValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.ContractState)
            .MaximumLength(10).WithMessage("Contract state should not be longer that 10 characters.");

        RuleFor(x => x.Microchip)
            .MaximumLength(100).WithMessage("Microchip should not exceed 100 characters.");

        RuleFor(x => x.CurrentDisease)
            .MaximumLength(500).WithMessage("CurrentDisease should not exceed 500 characters.");

        RuleFor(x => x.CurrentMedication)
            .MaximumLength(500).WithMessage("CurrentMedication should not exceed 500 characters.");

        RuleFor(x => x.PastDisease)
            .MaximumLength(500).WithMessage("PastDisease should not exceed 500 characters.");

        RuleFor(x => x.UsefulLinks)
            .Must(list => list.All(link => link.Length <= 200))
            .WithMessage("Each link in UsefulLinks must be 200 characters or less.");
    }
}

public class AnimalProfileDeleteCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileDeleteCommand>
{
    public AnimalProfileDeleteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");
    }
}
