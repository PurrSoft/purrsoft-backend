using FluentValidation;

namespace AlbumStore.Application.Commands.AnimalProfileCommands
{
    public class AnimalProfileCreateCommandValidator : AbstractValidator<PurrSoft.Application.Commands.AnimalProfileCommands.AnimalProfileCommands.AnimalProfileCreateCommand>
    {
        public AnimalProfileCreateCommandValidator()
        {
            RuleFor(x => x.CurrentDisease)
                .NotNull().WithMessage("CurrentDisease cannot be null.");

            RuleFor(x => x.CurrentMedication)
                .NotNull().WithMessage("CurrentMedication cannot be null.");

            RuleFor(x => x.PastDisease)
                .NotNull().WithMessage("PastDisease cannot be null.");
        }
    }

    public class AnimalProfileUpdateCommandValidator : AbstractValidator<PurrSoft.Application.Commands.AnimalProfileCommands.AnimalProfileCommands.AnimalProfileUpdateCommand>
    {
        public AnimalProfileUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("Id cannot be null or empty.");

            RuleFor(x => x.CurrentDisease)
                .NotNull().WithMessage("CurrentDisease cannot be null.");

            RuleFor(x => x.CurrentMedication)
                .NotNull().WithMessage("CurrentMedication cannot be null.");

            RuleFor(x => x.PastDisease)
                .NotNull().WithMessage("PastDisease cannot be null.");
        }
    }

    public class AnimalProfileDeleteCommandValidator : AbstractValidator<PurrSoft.Application.Commands.AnimalProfileCommands.AnimalProfileCommands.AnimalProfileDeleteCommand>
    {
        public AnimalProfileDeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty().WithMessage("Id cannot be null or empty.");
        }
    }
}