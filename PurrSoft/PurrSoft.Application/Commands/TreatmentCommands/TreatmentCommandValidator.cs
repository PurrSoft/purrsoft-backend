using FluentValidation;

namespace PurrSoft.Application.Commands.TreatmentCommands
{
    public class TreatmentCreateCommandValidator : AbstractValidator<TreatmentCommands.TreatmentCreateCommand>
    {
        public TreatmentCreateCommandValidator()
        {
            RuleFor(x => x.IdAnimal)
                .NotEmpty().WithMessage("IdAnimal is required.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("ImageUrl should not exceed 500 characters.")
                .Matches(@"^(http|https):\/\/").WithMessage("ImageUrl must be a valid URL.");

            RuleFor(x => x.MedicationName)
                .NotEmpty().WithMessage("MedicationName is required.")
                .MaximumLength(200).WithMessage("MedicationName should not exceed 200 characters.");

            RuleFor(x => x.MedicationTime)
                .NotEmpty().WithMessage("MedicationTime is required.")
                .MaximumLength(100).WithMessage("MedicationTime should not exceed 100 characters.");

            RuleFor(x => x.ExtraInfo)
                .MaximumLength(1000).WithMessage("ExtraInfo should not exceed 1000 characters.");

            RuleFor(x => x.TreatmentStart)
                .NotEmpty().WithMessage("TreatmentStart is required.")
                .LessThan(x => x.TreatmentEnd).WithMessage("TreatmentStart must be before TreatmentEnd.");

            RuleFor(x => x.TreatmentEnd)
                .NotEmpty().WithMessage("TreatmentEnd is required.");

            RuleFor(x => x.TreatmentDays)
                .GreaterThan(0).WithMessage("TreatmentDays must be greater than 0.");
        }
    }

    public class TreatmentUpdateCommandValidator : AbstractValidator<TreatmentCommands.TreatmentUpdateCommand>
    {
        public TreatmentUpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.IdAnimal)
                .NotEmpty().WithMessage("IdAnimal is required.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("ImageUrl should not exceed 500 characters.")
                .Matches(@"^(http|https):\/\/").WithMessage("ImageUrl must be a valid URL.");

            RuleFor(x => x.MedicationName)
                .NotEmpty().WithMessage("MedicationName is required.")
                .MaximumLength(200).WithMessage("MedicationName should not exceed 200 characters.");

            RuleFor(x => x.MedicationTime)
                .NotEmpty().WithMessage("MedicationTime is required.")
                .MaximumLength(100).WithMessage("MedicationTime should not exceed 100 characters.");

            RuleFor(x => x.ExtraInfo)
                .MaximumLength(1000).WithMessage("ExtraInfo should not exceed 1000 characters.");

            RuleFor(x => x.TreatmentStart)
                .NotEmpty().WithMessage("TreatmentStart is required.")
                .LessThan(x => x.TreatmentEnd).WithMessage("TreatmentStart must be before TreatmentEnd.");

            RuleFor(x => x.TreatmentEnd)
                .NotEmpty().WithMessage("TreatmentEnd is required.");

            RuleFor(x => x.TreatmentDays)
                .GreaterThan(0).WithMessage("TreatmentDays must be greater than 0.");
        }
    }

    public class TreatmentDeleteCommandValidator : AbstractValidator<TreatmentCommands.TreatmentDeleteCommand>
    {
        public TreatmentDeleteCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
}
