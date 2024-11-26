using FluentValidation;
using PurrSoft.Application.Helpers;
using PurrSoft.Domain.Entities.Enum;

namespace PurrSoft.Application.Commands.VolunteerCommands;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
	public CreateVolunteerCommandValidator()
	{
		RuleFor(x => x.VolunteerDto.UserId)
			.NotNull().NotEmpty();
		RuleFor(x => x.VolunteerDto.StartDate)
			.NotNull().NotEmpty()
			.Must(DateTimeValidationHelper.BeAValidDate)
			 .WithMessage("StartDate must be a valid date.")
			.Must(DateTimeValidationHelper.BeLaterThanMinValue)
			 .WithMessage("StartDate must be greater than the minimum date.");
		RuleFor(x => x.VolunteerDto.Status)
			.NotNull().NotEmpty()
			.Must(VolunteerCommandEnumValidator.BeAValidStatus);
		RuleFor(x => x.VolunteerDto.Tier)
			.NotNull().NotEmpty()
			.Must(VolunteerCommandEnumValidator.BeAValidTier);
	}
}

public class UpdateVolunteerCommandValidator : AbstractValidator<UpdateVolunteerCommand>
{
<<<<<<< HEAD
	public UpdateVolunteerCommandValidator()
	{
		RuleFor(x => x.VolunteerDto.UserId)
			.NotNull().NotEmpty();
		RuleFor(x => x.VolunteerDto.StartDate)
			.NotNull().NotEmpty()
			.Must(DateTimeValidationHelper.BeAValidDate)
			 .WithMessage("StartDate must be a valid date.")
			.Must(DateTimeValidationHelper.BeLaterThanMinValue)
			 .WithMessage("StartDate must be greater than the minimum date.");
		RuleFor(x => x.VolunteerDto.EndDate)
			.NotEmpty()
			.Must(DateTimeValidationHelper.BeAValidDate)
			.WithMessage("EndDate must be a valid date.")
			.Must((dto, endDate) =>
				DateTimeValidationHelper.BeLaterThan(dto.VolunteerDto.StartDate, endDate))
			.WithMessage("EndDate must be greater than StartDate.")
			.When(x => x.VolunteerDto.EndDate != null);
		RuleFor(x => x.VolunteerDto.Status)
			.NotNull().NotEmpty()
			.Must(VolunteerCommandEnumValidator.BeAValidStatus);
		RuleFor(x => x.VolunteerDto.Tier)
			.NotNull().NotEmpty()
			.Must(VolunteerCommandEnumValidator.BeAValidTier);
		RuleFor(x => x.VolunteerDto.Tasks)
			.NotNull().NotEmpty();
	}
=======
    public UpdateVolunteerCommandValidator()
    {
        RuleFor(x => x.VolunteerDto.UserId)
            .NotNull().NotEmpty();
        RuleFor(x => DateTime.Parse(x.VolunteerDto.StartDate))
            .NotNull().NotEmpty()
            .GreaterThan(DateTime.MinValue);
        RuleFor(x => DateTime.Parse(x.VolunteerDto.EndDate))
            .NotNull().NotEmpty()
            .GreaterThan(v => DateTime.Parse(v.VolunteerDto.StartDate))
            .When(v => v.VolunteerDto.EndDate != null);
        RuleFor(x => x.VolunteerDto.Status)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidStatus);
        RuleFor(x => x.VolunteerDto.Tier)
            .NotNull().NotEmpty()
            .Must(VolunteerCommandEnumValidator.BeAValidTier);
    }
>>>>>>> main
}

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
	public DeleteVolunteerCommandValidator()
	{
		RuleFor(x => x.Id)
			.NotNull().NotEmpty();
	}
}

public static class VolunteerCommandEnumValidator
{
	public static bool BeAValidStatus(string status)
	{
		return Enum.TryParse(typeof(VolunteerStatus), status, true, out _);
	}

	public static bool BeAValidTier(string tier)
	{
		return Enum.TryParse(typeof(TierLevel), tier, true, out _);
	}
}