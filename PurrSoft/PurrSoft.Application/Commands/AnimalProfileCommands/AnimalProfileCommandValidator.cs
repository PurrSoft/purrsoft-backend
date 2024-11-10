using FluentValidation;

namespace PurrSoft.Application.Commands.AnimalProfileCommands
{
    public class AnimalProfileCreateCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileCreateCommand>
    {
        public AnimalProfileCreateCommandValidator()
        {
            RuleFor(x => x.AnimalId)
                .NotEmpty().WithMessage("AnimalId is required.");

            RuleFor(x => x.CurrentDisease)
                .NotEmpty().WithMessage("CurrentDisease cannot be empty.")
                .MaximumLength(500).WithMessage("CurrentDisease should not exceed 500 characters.");

            RuleFor(x => x.CurrentMedication)
                .NotEmpty().WithMessage("CurrentMedication cannot be empty.")
                .MaximumLength(500).WithMessage("CurrentMedication should not exceed 500 characters.");

            RuleFor(x => x.PastDisease)
                .NotEmpty().WithMessage("PastDisease cannot be empty.")
                .MaximumLength(500).WithMessage("PastDisease should not exceed 500 characters.");
        }
    }

    public class AnimalProfileUpdateCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileUpdateCommand>
    {
        public AnimalProfileUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be null or empty.");

            RuleFor(x => x.CurrentDisease)
                .NotEmpty().WithMessage("CurrentDisease cannot be empty.")
                .MaximumLength(500).WithMessage("CurrentDisease should not exceed 500 characters.");

            RuleFor(x => x.CurrentMedication)
                .NotEmpty().WithMessage("CurrentMedication cannot be empty.")
                .MaximumLength(500).WithMessage("CurrentMedication should not exceed 500 characters.");

            RuleFor(x => x.PastDisease)
                .NotEmpty().WithMessage("PastDisease cannot be empty.")
                .MaximumLength(500).WithMessage("PastDisease should not exceed 500 characters.");
        }
    }

    public class AnimalProfileDeleteCommandValidator : AbstractValidator<AnimalProfileCommands.AnimalProfileDeleteCommand>
    {
        public AnimalProfileDeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be null or empty.");
        }
    }
}
